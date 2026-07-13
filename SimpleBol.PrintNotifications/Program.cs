using System.Diagnostics;
using System.IO.Pipes;
using System.Management;
using System.Text.Json;

namespace SimpleBol.PrintNotifications;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        using var mutex = new Mutex(
            initiallyOwned: true,
            name: @"Global\SimpleBol.PrintNotifications",
            createdNew: out bool createdNew);
        if (!createdNew)
            return;

        ApplicationConfiguration.Initialize();
        Application.Run(new PrintNotificationContext());
    }
}

internal sealed class PrintNotificationContext : ApplicationContext
{
    private const string PipeName = "SimpleBol.PrintNotifications";
    private static readonly TimeSpan UnseenJobTimeout = TimeSpan.FromSeconds(20);

    private readonly NotifyIcon _notifyIcon;
    private readonly Icon? _applicationIcon;
    private readonly SynchronizationContext _syncContext;
    private readonly CancellationTokenSource _shutdown = new();
    private readonly System.Windows.Forms.Timer _queueTimer = new() { Interval = 750 };
    private readonly Dictionary<Guid, PendingPrintJob> _pendingJobs = [];

    public PrintNotificationContext()
    {
        _syncContext = SynchronizationContext.Current ?? new WindowsFormsSynchronizationContext();
        _applicationIcon = LoadApplicationIcon();
        _notifyIcon = new NotifyIcon
        {
            Icon = _applicationIcon ?? SystemIcons.Information,
            Text = "SimpleBol Print Notifications",
            Visible = true,
            ContextMenuStrip = BuildMenu()
        };

        _queueTimer.Tick += (_, _) => CheckPrintQueue();
        _queueTimer.Start();
        _ = ListenForCommandsAsync(_shutdown.Token);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _shutdown.Cancel();
            _queueTimer.Dispose();
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _applicationIcon?.Dispose();
            _shutdown.Dispose();
        }

        base.Dispose(disposing);
    }

    private ContextMenuStrip BuildMenu()
    {
        var menu = new ContextMenuStrip();
        menu.Items.Add("Check SimpleBol print jobs", null, (_, _) => ShowPendingStatus());
        menu.Items.Add("Open Printers & scanners", null, (_, _) => OpenPrintersSettings());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Exit", null, (_, _) => ExitThread());
        return menu;
    }

    private async Task ListenForCommandsAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await using var pipe = new NamedPipeServerStream(
                    PipeName,
                    PipeDirection.In,
                    1,
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
                await pipe.WaitForConnectionAsync(cancellationToken);
                using var reader = new StreamReader(pipe);
                string? json = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(json))
                    continue;

                PrintNotificationCommand? command =
                    JsonSerializer.Deserialize<PrintNotificationCommand>(json);
                if (command is null ||
                    string.IsNullOrWhiteSpace(command.PrinterName) ||
                    string.IsNullOrWhiteSpace(command.DocumentName))
                    continue;

                _syncContext.Post(_ => RegisterPrintJob(command), null);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SimpleBol print notification pipe error: {ex}");
                try
                {
                    await Task.Delay(500, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }

    private void RegisterPrintJob(PrintNotificationCommand command)
    {
        _pendingJobs[command.RequestId] = new PendingPrintJob(command);
        string copies = command.Copies == 1 ? "1 copy" : $"{command.Copies} copies";
        ShowNotification(
            "SimpleBol print submitted",
            $"{command.DocumentName}{Environment.NewLine}{command.PrinterName} - {copies}",
            ToolTipIcon.Info);
        CheckPrintQueue();
    }

    private void CheckPrintQueue()
    {
        if (_pendingJobs.Count == 0)
            return;

        IReadOnlyList<QueueJobSnapshot> queueJobs;
        try
        {
            queueJobs = ReadQueueJobs();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to read the Windows print queue: {ex}");
            return;
        }

        var claimedQueueJobs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (PendingPrintJob pending in _pendingJobs.Values.Where(job => !job.SeenInQueue))
        {
            QueueJobSnapshot? match = queueJobs.FirstOrDefault(job =>
                !claimedQueueJobs.Contains(job.Name) &&
                job.PrinterName.Equals(pending.Command.PrinterName, StringComparison.OrdinalIgnoreCase) &&
                job.DocumentName.Equals(pending.Command.DocumentName, StringComparison.OrdinalIgnoreCase));
            if (match is null)
                continue;

            pending.SeenInQueue = true;
            pending.QueueJobName = match.Name;
            claimedQueueJobs.Add(match.Name);
        }

        var completedRequestIds = new List<Guid>();
        foreach ((Guid requestId, PendingPrintJob pending) in _pendingJobs)
        {
            if (!pending.SeenInQueue)
            {
                if (DateTime.UtcNow - pending.ReceivedUtc >= UnseenJobTimeout)
                {
                    ShowNotification(
                        "Print job accepted",
                        $"{pending.Command.DocumentName}{Environment.NewLine}{pending.Command.PrinterName}",
                        ToolTipIcon.Info);
                    completedRequestIds.Add(requestId);
                }

                continue;
            }

            QueueJobSnapshot? current = queueJobs.FirstOrDefault(job =>
                job.Name.Equals(pending.QueueJobName, StringComparison.OrdinalIgnoreCase));
            if (current is null)
            {
                ShowNotification(
                    pending.HadProblem ? "SimpleBol print failed" : "SimpleBol print completed",
                    $"{pending.Command.DocumentName}{Environment.NewLine}{pending.Command.PrinterName}",
                    pending.HadProblem ? ToolTipIcon.Error : ToolTipIcon.Info);
                completedRequestIds.Add(requestId);
                continue;
            }

            if (current.HasProblem && !pending.HadProblem)
            {
                pending.HadProblem = true;
                ShowNotification(
                    "SimpleBol printer needs attention",
                    $"{pending.Command.DocumentName}{Environment.NewLine}{current.StatusText}",
                    ToolTipIcon.Error);
            }
        }

        foreach (Guid requestId in completedRequestIds)
            _pendingJobs.Remove(requestId);
    }

    private static IReadOnlyList<QueueJobSnapshot> ReadQueueJobs()
    {
        const string query =
            "SELECT Name, Document, JobStatus, Status, StatusMask FROM Win32_PrintJob";
        using var searcher = new ManagementObjectSearcher(query);
        using ManagementObjectCollection results = searcher.Get();
        var jobs = new List<QueueJobSnapshot>();

        foreach (ManagementObject result in results)
        {
            string name = result["Name"]?.ToString() ?? string.Empty;
            string document = result["Document"]?.ToString() ?? string.Empty;
            if (name.Length == 0 || document.Length == 0)
                continue;

            int separator = name.LastIndexOf(',');
            string printerName = separator > 0 ? name[..separator].Trim() : name;
            string jobStatus = result["JobStatus"]?.ToString() ?? string.Empty;
            string status = result["Status"]?.ToString() ?? string.Empty;
            int statusMask = Convert.ToInt32(result["StatusMask"] ?? 0);
            string statusText = string.Join(
                " - ",
                new[] { jobStatus, status }.Where(value => !string.IsNullOrWhiteSpace(value)));

            jobs.Add(new QueueJobSnapshot
            {
                Name = name,
                PrinterName = printerName,
                DocumentName = document,
                StatusText = string.IsNullOrWhiteSpace(statusText) ? "Printer error" : statusText,
                HasProblem = HasProblemStatus(statusMask, statusText)
            });
        }

        return jobs;
    }

    private static bool HasProblemStatus(int statusMask, string statusText)
    {
        const int problemMask = 2 | 32 | 64 | 512 | 1024;
        if ((statusMask & problemMask) != 0)
            return true;

        string[] problemTerms = ["error", "offline", "paper out", "blocked", "user intervention"];
        return problemTerms.Any(term =>
            statusText.Contains(term, StringComparison.OrdinalIgnoreCase));
    }

    private void ShowPendingStatus()
    {
        if (_pendingJobs.Count == 0)
        {
            ShowNotification(
                "SimpleBol Print Notifications",
                "No SimpleBol print jobs are currently being tracked.",
                ToolTipIcon.Info);
            return;
        }

        int queued = _pendingJobs.Values.Count(job => job.SeenInQueue);
        ShowNotification(
            "SimpleBol Print Notifications",
            $"Tracking {_pendingJobs.Count} job(s); {queued} currently visible in a printer queue.",
            ToolTipIcon.Info);
    }

    private void ShowNotification(string title, string message, ToolTipIcon icon)
    {
        _notifyIcon.BalloonTipTitle = title;
        _notifyIcon.BalloonTipText = message;
        _notifyIcon.BalloonTipIcon = icon;
        _notifyIcon.ShowBalloonTip(7000);
    }

    private static void OpenPrintersSettings()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "ms-settings:printers",
            UseShellExecute = true
        });
    }

    private static Icon? LoadApplicationIcon()
    {
        string appPath = Path.Combine(AppContext.BaseDirectory, "SimpleBol.exe");
        if (!File.Exists(appPath))
            return null;

        using Icon? extracted = Icon.ExtractAssociatedIcon(appPath);
        return extracted is null ? null : (Icon)extracted.Clone();
    }
}

internal sealed class PendingPrintJob(PrintNotificationCommand command)
{
    public PrintNotificationCommand Command { get; } = command;
    public DateTime ReceivedUtc { get; } = DateTime.UtcNow;
    public bool SeenInQueue { get; set; }
    public bool HadProblem { get; set; }
    public string QueueJobName { get; set; } = string.Empty;
}

internal sealed class PrintNotificationCommand
{
    public Guid RequestId { get; init; }
    public string PrinterName { get; init; } = string.Empty;
    public string DocumentName { get; init; } = string.Empty;
    public int Copies { get; init; }
    public DateTime SubmittedUtc { get; init; }
}

internal sealed class QueueJobSnapshot
{
    public string Name { get; init; } = string.Empty;
    public string PrinterName { get; init; } = string.Empty;
    public string DocumentName { get; init; } = string.Empty;
    public string StatusText { get; init; } = string.Empty;
    public bool HasProblem { get; init; }
}
