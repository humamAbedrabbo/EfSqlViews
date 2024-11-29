using EfSqlViews.ApplicationCore.Abstractions;

namespace EfSqlViews.ApplicationCore.Features.Sites;

public class SiteListView : ISqlView
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
