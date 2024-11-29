namespace EfSqlViews.ApplicationCore.Domain.Abstractions;
public interface ICurrentUser
{
    string? GetCurrentUserName();
}
