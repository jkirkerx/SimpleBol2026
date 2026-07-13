using SimpleBol.Models.MongoDb;

namespace SimpleBol.Services;

public interface ICurrentUserSession
{
    ACCOUNTS? Account { get; }
    bool IsAuthenticated { get; }
    void SignIn(ACCOUNTS account);
    void SignOut();
}

public sealed class CurrentUserSession : ICurrentUserSession
{
    public ACCOUNTS? Account { get; private set; }
    public bool IsAuthenticated => Account is not null;

    public void SignIn(ACCOUNTS account)
    {
        ArgumentNullException.ThrowIfNull(account);
        Account = account;
    }

    public void SignOut() => Account = null;
}
