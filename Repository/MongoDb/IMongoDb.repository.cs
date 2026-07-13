using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Setup;

namespace SimpleBol.Repository.MongoDb
{

    public interface IMongoDbRepository
    {
        Task<MongoConnectionTestResult> TestConnectionAsync(
            CancellationToken cancellationToken = default);
        Task<MongoConnectionTestResult> TestConnectionStringAsync(
            string connectionString,
            string databaseName,
            CancellationToken cancellationToken = default);
        Task InitializeDatabaseAsync(CancellationToken cancellationToken = default);
    }

    public sealed record MongoConnectionTestResult(bool Success, string Message)
    {
        public static MongoConnectionTestResult Passed() =>
            new(true, "MongoDB connected successfully.");
    }

    public class MongoDbRepository: IMongoDbRepository
    {

        private readonly MongoDbContext _context;

        public MongoDbRepository(MongoDbContext context)
        {
            _context = context;
        }

        public Task<MongoConnectionTestResult> TestConnectionAsync(
            CancellationToken cancellationToken = default)
        {
            return TestDatabaseAsync(_context.Database, cancellationToken);
        }

        public async Task<MongoConnectionTestResult> TestConnectionStringAsync(
            string connectionString,
            string databaseName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                    return new(false, "The MongoDB connection string is empty.");
                if (string.IsNullOrWhiteSpace(databaseName))
                    return new(false, "The MongoDB database name is empty.");

                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                return await TestDatabaseAsync(database, cancellationToken);
            }
            catch (Exception ex) when (IsConnectionException(ex))
            {
                return DescribeConnectionFailure(ex);
            }
        }

        private static async Task<MongoConnectionTestResult> TestDatabaseAsync(
            IMongoDatabase database,
            CancellationToken cancellationToken)
        {
            using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutSource.CancelAfter(MongoDbDefaults.ConnectionTimeout);

            try
            {
                await database.RunCommandAsync<BsonDocument>(
                    new BsonDocument("ping", 1),
                    cancellationToken: timeoutSource.Token);

                // A ping alone does not prove that the authenticated user can access
                // the selected database. Listing names verifies database-level access
                // without modifying user data.
                using var cursor = await database.ListCollectionNamesAsync(
                    cancellationToken: timeoutSource.Token);
                _ = await cursor.ToListAsync(timeoutSource.Token);
                return MongoConnectionTestResult.Passed();
            }
            catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                return DescribeConnectionFailure(new TimeoutException(
                    "The MongoDB connection test exceeded the configured timeout.", ex));
            }
            catch (Exception ex) when (IsConnectionException(ex))
            {
                return DescribeConnectionFailure(ex);
            }
        }

        private static bool IsConnectionException(Exception exception) =>
            exception is MongoException or TimeoutException or FormatException or ArgumentException;

        internal static MongoConnectionTestResult DescribeConnectionFailure(Exception exception)
        {
            ErrorLogging.NLogException(exception, "MongoDB connection test");
            var message = exception switch
            {
                MongoAuthenticationException =>
                    "Authentication failed. Check the user name, password, authentication database, and mechanism.",
                MongoConfigurationException =>
                    "The MongoDB connection settings are invalid. Check the host, port, and connection string.",
                MongoConnectionException =>
                    "The MongoDB server could not be reached. Confirm the service is running and the host and port are correct.",
                TimeoutException =>
                    "The MongoDB connection timed out. Confirm the server is reachable and accepting connections.",
                MongoCommandException commandException when commandException.Code == 13 =>
                    "The user connected but does not have permission to access this database.",
                _ when exception.Message.Contains("timed out", StringComparison.OrdinalIgnoreCase) =>
                    "The MongoDB connection timed out. Confirm the server is reachable and accepting connections.",
                _ => $"MongoDB rejected the connection: {exception.Message}"
            };
            return new MongoConnectionTestResult(false, message);
        }

        public Task InitializeDatabaseAsync(CancellationToken cancellationToken = default) =>
            MongoDbInitializer.InitializeAsync(_context.Database, cancellationToken);
    }

}
