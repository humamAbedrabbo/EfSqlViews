using EfSqlViews.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EfSqlViews.Infrastructure;
public sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.ApplySqlViewConfiguration();

    }
}
