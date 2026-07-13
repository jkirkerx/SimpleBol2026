using System.Security.Cryptography;
using System.Text;

namespace SimpleBol.Classes.Common;

internal static class WindowsDataProtection
{
    private const string Prefix = "dpapi:";
    private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("SimpleBol.MongoDb.Password.v1");

    public static string Protect(string value)
    {
        var encrypted = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(value), Entropy, DataProtectionScope.CurrentUser);
        return Prefix + Convert.ToBase64String(encrypted);
    }

    public static string Unprotect(string value)
    {
        if (!value.StartsWith(Prefix, StringComparison.Ordinal))
            return value;

        var decrypted = ProtectedData.Unprotect(
            Convert.FromBase64String(value[Prefix.Length..]),
            Entropy,
            DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(decrypted);
    }
}
