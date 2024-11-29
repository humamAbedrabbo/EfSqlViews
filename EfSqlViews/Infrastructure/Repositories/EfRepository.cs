using Ardalis.Specification.EntityFrameworkCore;
using EfSqlViews.ApplicationCore.Domain.Abstractions;

namespace EfSqlViews.Infrastructure.Repositories;
public class EfRepository<T>(ApplicationDbContext dbContext)
    : RepositoryBase<T>(dbContext), IRepository<T>
    where T : class
{
}
