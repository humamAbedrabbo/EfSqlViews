using EfSqlViews.ApplicationCore.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EfSqlViews.Infrastructure.Extensions;
public static class ModelBuilderExtensions
{
    public static List<Type> GetSqlViewImplementations(this ModelBuilder modelBuilder)
    {
        return typeof(ISqlViewImplementation<>).Assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISqlViewImplementation<>)))
            .ToList();
    }

    public static List<Type> GetSqlViewEntityTypes(this ModelBuilder modelBuilder)
    {
        return typeof(ISqlView).Assembly.GetTypes()
                .Where(t => t.IsClass && typeof(ISqlView).IsAssignableFrom(t))
                .ToList();
    }

    public static void ApplySqlViewConfiguration(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.GetSqlViewEntityTypes();

        foreach (var type in entityTypes)
        {
            var entity = modelBuilder.Entity(type);
            entity.HasNoKey();
            entity.ToView(type.Name);
        }
    }
}

