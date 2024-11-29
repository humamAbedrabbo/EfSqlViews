using EfSqlViews.ApplicationCore.Domain.Abstractions;

namespace EfSqlViews.Infrastructure.Services;

public class CurrentUserService : ICurrentUser
{
    public string? GetCurrentUserName() => "user-name";
}
