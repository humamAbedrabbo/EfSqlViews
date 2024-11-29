using EfSqlViews.ApplicationCore.Abstractions;
using EfSqlViews.ApplicationCore.Features.Sites;

namespace EfSqlViews.Infrastructure.Features.Sites;
public class SiteListViewSqlImplementation : ISqlViewImplementation<SiteListView>
{
    public static string GetSql()
        => """
        IF NOT EXISTS (
            SELECT * 
            FROM sys.views 
            WHERE object_id = OBJECT_ID(N'dbo.SiteListView')
        )
        BEGIN
            Exec('
            CREATE VIEW dbo.SiteListView
            AS
                SELECT TOP (100) PERCENT 
                    Id 
                    ,Name
                FROM dbo.Sites
                ORDER BY Name
            ');
        END;
        """;
}
