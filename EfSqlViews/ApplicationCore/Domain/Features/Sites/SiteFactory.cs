using EfSqlViews.ApplicationCore.Domain.Abstractions;

namespace EfSqlViews.ApplicationCore.Domain.Features.Sites;

public class SiteFactory : ISiteFactory
{
    private readonly TimeProvider timeProvider;
    private readonly ICurrentUser currentUser;

    public SiteFactory(TimeProvider timeProvider, ICurrentUser currentUser)
    {
        this.timeProvider = timeProvider;
        this.currentUser = currentUser;
    }

    public Site CreateSite(string name)
    {
        return new Site(name, timeProvider, currentUser);
    }
}
