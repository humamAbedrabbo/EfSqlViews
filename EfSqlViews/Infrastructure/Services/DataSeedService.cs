using System.Reflection;
using EfSqlViews.ApplicationCore.Domain.Features.Sites;
using EfSqlViews.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EfSqlViews.Infrastructure.Services;
public class DataSeedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await db.Database.MigrateAsync(cancellationToken)
            .ConfigureAwait(false);

        await CreateDatabaseViews(db, cancellationToken);

        await Seed_Sites(scope, db, cancellationToken);
    }

    private static async Task CreateDatabaseViews(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var modelBuilder = new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet());

        var entityTypes = modelBuilder.GetSqlViewImplementations();

        foreach (var entityType in entityTypes)
        {
            var methodInfo = entityType.GetMethod("GetSql", BindingFlags.Static | BindingFlags.Public);

            if (methodInfo != null)
            {
                var sql = methodInfo.Invoke(null, null) as string;
                await db.Database.ExecuteSqlRawAsync(sql!, cancellationToken);
            }
        }

        // Apply the SQL view configuration
        modelBuilder.ApplySqlViewConfiguration();
    }

    private static async Task Seed_Sites(IServiceScope scope, ApplicationDbContext db, CancellationToken cancellationToken)
    {
        if (!db.Set<Site>().Any())
        {
            var siteFactory = scope.ServiceProvider.GetRequiredService<ISiteFactory>();
            List<Site> sites = [];
            for (int i = 1; i <= 9; i++)
            {
                sites.Add(siteFactory.CreateSite($"Site-{i}"));
            }
            db.Set<Site>().AddRange(sites);
            await db.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
