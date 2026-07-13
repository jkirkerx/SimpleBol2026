using System.Reflection;

namespace SimpleBol;

internal static class AppInfo
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static string Name => Application.ProductName!;

    public static string Version => Application.ProductVersion;

    public static string Company =>
        Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;

    public static string Copyright =>
        $"{Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright} {DateTime.Now.Year}".Trim();

    public static string BuildTitle => $"{Name} - Build: {Version}";

    public static string WindowTitle(string area) => $"{Name} - {area}";
}
