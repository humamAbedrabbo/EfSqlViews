namespace EfSqlViews.ApplicationCore.Abstractions;
public interface ISqlViewImplementation<T> where T : class
{
    static string GetSql() => throw new NotImplementedException();
}
