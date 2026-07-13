using MongoDB.Driver;
using SimpleBol.Classes.Common;
using SimpleBol.Models;

namespace SimpleBol.Setup;

internal static class MongoDbConnectionFactory
{
    public static MongoClient CreateClient(DbConnection settings)
    {
        // Preserve compatibility with existing user-supplied connection strings.
        if (!settings.PasswordProtected && !string.IsNullOrWhiteSpace(settings.Connection))
            return new MongoClient(settings.Connection);

        if (string.IsNullOrWhiteSpace(settings.Host))
            throw new InvalidOperationException("The MongoDB host is not configured.");

        if (!int.TryParse(settings.Port, out var port))
            throw new InvalidOperationException("The MongoDB port is invalid.");

        var builder = new MongoUrlBuilder
        {
            Server = new MongoServerAddress(settings.Host, port),
            DatabaseName = settings.Database,
            ServerSelectionTimeout = MongoDbDefaults.ConnectionTimeout
        };

        if (!string.IsNullOrWhiteSpace(settings.User))
        {
            builder.Username = settings.User;
            builder.Password = WindowsDataProtection.Unprotect(settings.Pass ?? string.Empty);
            builder.AuthenticationSource = settings.AuthDatabase;
            builder.AuthenticationMechanism = settings.AuthMechanism;
        }

        return new MongoClient(builder.ToMongoUrl());
    }
}
