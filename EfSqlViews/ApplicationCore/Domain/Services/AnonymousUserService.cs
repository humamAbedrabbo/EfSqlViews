using EfSqlViews.ApplicationCore.Domain.Abstractions;

namespace EfSqlViews.ApplicationCore.Domain.Services;

internal class AnonymousUserService : ICurrentUser
{
    public string? GetCurrentUserName() => null;
}
