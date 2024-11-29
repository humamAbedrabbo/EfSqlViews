namespace EfSqlViews.ApplicationCore.Domain.Abstractions;

public abstract class BaseEntity<T>(TimeProvider timeProvider, ICurrentUser currentUser)
    where T : struct
{
    protected readonly TimeProvider timeProvider = timeProvider;
    protected readonly ICurrentUser currentUser = currentUser;

    public T Id { get; init; }
    public DateTimeOffset CreatedOn { get; }
    public DateTimeOffset LastUpdated { get; protected set; }
    public string? LastUpdatedBy { get; protected set; }

    protected void RefreshLastUpdate()
    {
        LastUpdated = timeProvider.GetLocalNow();
        LastUpdatedBy = currentUser.GetCurrentUserName();
    }

    protected void RefreshLastUpdateOnly()
    {
        LastUpdated = timeProvider.GetLocalNow();
    }
}
