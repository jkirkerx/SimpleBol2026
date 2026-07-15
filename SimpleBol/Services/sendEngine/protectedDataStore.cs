using Google.Apis.Json;
using Google.Apis.Util.Store;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SimpleBol.Services.sendEngine
{
    internal sealed class ProtectedDataStore : IDataStore
    {
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("SimpleBol.Gmail.OAuth");
        private readonly string folderPath;

        public ProtectedDataStore()
        {
            folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SimpleBol", "GmailAuth");
            Directory.CreateDirectory(folderPath);
        }

        public Task StoreAsync<T>(string key, T value)
        {
            var json = NewtonsoftJsonSerializer.Instance.Serialize(value);
            var clearBytes = Encoding.UTF8.GetBytes(json);
            var protectedBytes = ProtectedData.Protect(clearBytes, Entropy,
                DataProtectionScope.CurrentUser);
            File.WriteAllBytes(GetPath(key), protectedBytes);
            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(string key)
        {
            var path = GetPath(key);
            if (File.Exists(path))
                File.Delete(path);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            var path = GetPath(key);
            if (!File.Exists(path))
                return Task.FromResult(default(T));

            var protectedBytes = File.ReadAllBytes(path);
            var clearBytes = ProtectedData.Unprotect(protectedBytes, Entropy,
                DataProtectionScope.CurrentUser);
            var value = NewtonsoftJsonSerializer.Instance.Deserialize<T>(
                Encoding.UTF8.GetString(clearBytes));
            return Task.FromResult<T?>(value);
        }

        public Task ClearAsync()
        {
            if (Directory.Exists(folderPath))
            {
                foreach (var file in Directory.EnumerateFiles(folderPath, "*.token"))
                    File.Delete(file);
            }
            return Task.CompletedTask;
        }

        private string GetPath(string key)
        {
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(key)));
            return Path.Combine(folderPath, $"{hash}.token");
        }
    }
}
