using System.IO;

namespace SimpleBol.Setup;

internal static class MongoDbInstallerInfo
{
    public const string ServerVersion = "8.0.26";
    public const string ServerVersionSeries = "8.0";
    public const string InstallerFileName = "mongodb-windows-x86_64-8.0.26-signed.msi";
    public const string InstallerDownloadUrl =
        "https://fastdl.mongodb.org/windows/mongodb-windows-x86_64-8.0.26-signed.msi";

    public static string InstallPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        "MongoDB", "Server", ServerVersionSeries);
}
