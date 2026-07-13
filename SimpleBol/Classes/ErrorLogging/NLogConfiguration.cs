using System.Text;
using System.IO;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace SimpleBol.Classes.Errors;

internal static class NLogConfiguration
{
    private const long ArchiveSizeBytes = 5_000_000;
    private const int MaximumArchiveFiles = 5;

    public static string LogDirectory { get; private set; } = string.Empty;

    public static void Configure()
    {
        LogDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SimpleBol",
            "ErrorLogs");
        Directory.CreateDirectory(LogDirectory);

        InternalLogger.LogLevel = LogLevel.Warn;
        InternalLogger.LogFile = Path.Combine(LogDirectory, "nLog.txt");

        var configuration = new LoggingConfiguration();
        var debuggerTarget = CreateFileTarget(
            "Debugger",
            "Debugger.json",
            CreateJsonLayout(includeException: false));
        var informationTarget = CreateFileTarget(
            "Information",
            "Information.json",
            CreateJsonLayout(includeException: false));
        var applicationTarget = CreateFileTarget(
            "Application",
            "Application.json",
            CreateJsonLayout(includeException: true));
        var fatalTarget = CreateFileTarget(
            "Fatal",
            "Fatal.json",
            CreateJsonLayout(includeException: true));

        configuration.AddRule(LogLevel.Trace, LogLevel.Debug, debuggerTarget);
        configuration.AddRule(LogLevel.Info, LogLevel.Warn, informationTarget);
        configuration.AddRule(LogLevel.Error, LogLevel.Error, applicationTarget);
        configuration.AddRule(LogLevel.Fatal, LogLevel.Fatal, fatalTarget);

        LogManager.Configuration = configuration;
    }

    private static FileTarget CreateFileTarget(
        string name,
        string fileName,
        Layout layout)
    {
        return new FileTarget(name)
        {
            FileName = Path.Combine(LogDirectory, fileName),
            Layout = layout,
            Encoding = Encoding.UTF8,
            KeepFileOpen = false,
            ArchiveAboveSize = ArchiveSizeBytes,
            MaxArchiveFiles = MaximumArchiveFiles
        };
    }

    private static JsonLayout CreateJsonLayout(bool includeException)
    {
        var nestedLayout = new JsonLayout
        {
            SuppressSpaces = true
        };
        nestedLayout.Attributes.Add(new JsonAttribute(
            "Message",
            includeException ? "${callsite} - ${message}" : "${message}"));

        if (includeException)
        {
            nestedLayout.Attributes.Add(new JsonAttribute(
                "Exception",
                "${exception:format=tostring}"));
        }

        var layout = new JsonLayout
        {
            SuppressSpaces = true
        };
        layout.Attributes.Add(new JsonAttribute(
            "Time",
            "${date:format=MM/dd/yyyy - HH\\:mm\\:ss}"));
        layout.Attributes.Add(new JsonAttribute(
            "Level",
            "${level:uppercase=true}"));
        layout.Attributes.Add(new JsonAttribute("Nested", nestedLayout));
        return layout;
    }
}
