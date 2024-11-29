# EfSqlViews

EfSqlViews is a demo of implementing sql views using entity framework core.


## Getting Started

To get started with the project, clone the repository and run the following commands:

* Create a DTO that represents a sql view
```c#
public class SiteListView : ISqlView
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
```

* Create a sql view implementation
```c#
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
```
* Create an EndPoint that will query the view
```c#
app.MapGet("/", async ([FromServices] IRepository<SiteListView> repo) =>
{
    return await repo.ListAsync();
})
.WithName("GetSites");
```
* Build and run

```bash
dotnet restore
dotnet build
dotnet run



