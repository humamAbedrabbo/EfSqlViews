using EfSqlViews.ApplicationCore.Domain.Abstractions;
using EfSqlViews.ApplicationCore.Domain.Services;

namespace EfSqlViews.ApplicationCore.Domain.Features.Sites;

public sealed class Site : BaseEntity<Guid>, IAggregateEntity
{
    public string Name { get; private set; } = default!;

    private Site()
        : base(TimeProvider.System, new AnonymousUserService())
    {
    }

    internal Site(string name
        , TimeProvider timeProvider
        , ICurrentUser currentUser)

        : base(timeProvider, currentUser)
    {
        Id = Guid.CreateVersion7();
        Name = name;
    }

    public void ChangeName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName?.Trim(), nameof(newName));
        Name = newName!.Trim();
        RefreshLastUpdate();
    }
}
