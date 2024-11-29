using Ardalis.Specification;

namespace EfSqlViews.ApplicationCore.Domain.Abstractions;

public interface IRepository<T> : IReadRepositoryBase<T>
    where T : class;
