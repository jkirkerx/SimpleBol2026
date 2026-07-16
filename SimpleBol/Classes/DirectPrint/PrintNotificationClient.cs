using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text.Json;

namespace SimpleBol.Classes.DirectPrint;

internal static class PrintNotificationClient
{
    private const string PipeName = "SimpleBol.PrintNotifications";

    public static async Task EnsureRunningAsync()
    {
        if (Process.GetProcessesByName("SimpleBol.PrintNotifications").Length > 0)
            return;

        string? executablePath = FindNotificationExecutable();
        if (executablePath is null)
            return;

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = executablePath,
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(executablePath)!
            });

            for (int attempt = 0; attempt < 15; attempt++)
            {
                await Task.Delay(100);
                if (Process.GetProcessesByName("SimpleBol.PrintNotifications").Length > 0)
                    return;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to start SimpleBol print notifications: {ex}");
        }
    }

    public static async Task NotifySubmittedAsync(
        string printerName,
        string documentName,
        short copies)
    {
        var command = new PrintNotificationCommand
        {
            RequestId = Guid.NewGuid(),
            PrinterName = printerName,
            DocumentName = documentName,
            Copies = Math.Max(1, (int)copies),
            SubmittedUtc = DateTime.UtcNow
        };

        await SendCommandAsync(command);
    }

    public static async Task NotifyEmailSentAsync(
        string documentName,
        IEnumerable<string> recipients)
    {
        var command = new PrintNotificationCommand
        {
            RequestId = Guid.NewGuid(),
            NotificationType = "EmailSent",
            DocumentName = documentName,
            Recipients = recipients.Where(recipient => !string.IsNullOrWhiteSpace(recipient)).ToList(),
            SubmittedUtc = DateTime.UtcNow
        };

        await SendCommandAsync(command);
    }

    private static async Task SendCommandAsync(PrintNotificationCommand command)
    {
        string payload = JsonSerializer.Serialize(command);

        for (int attempt = 0; attempt < 5; attempt++)
        {
            try
            {
                using var pipe = new NamedPipeClientStream(
                    ".",
                    PipeName,
                    PipeDirection.Out,
                    PipeOptions.Asynchronous);
                await pipe.ConnectAsync(500);
                using var writer = new StreamWriter(pipe) { AutoFlush = true };
                await writer.WriteLineAsync(payload);
                return;
            }
            catch (Exception ex) when (ex is TimeoutException or IOException)
            {
                if (attempt == 4)
                    Debug.WriteLine($"Unable to notify the print monitor: {ex.Message}");
                else
                    await Task.Delay(150);
            }
        }
    }

    private static string? FindNotificationExecutable()
    {
        string installedPath = Path.Combine(
            AppContext.BaseDirectory,
            "SimpleBol.PrintNotifications.exe");
        if (File.Exists(installedPath))
            return installedPath;

        string configuration =
#if DEBUG
            "Debug";
#else
            "Release";
#endif

        string developmentPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "SimpleBol.PrintNotifications",
            "bin",
            configuration,
            "net10.0-windows",
            "SimpleBol.PrintNotifications.exe"));

        return File.Exists(developmentPath) ? developmentPath : null;
    }

    private sealed class PrintNotificationCommand
    {
        public Guid RequestId { get; init; }
        public string NotificationType { get; init; } = "PrintSubmitted";
        public string PrinterName { get; init; } = string.Empty;
        public string DocumentName { get; init; } = string.Empty;
        public int Copies { get; init; }
        public List<string> Recipients { get; init; } = [];
        public DateTime SubmittedUtc { get; init; }
    }
}
