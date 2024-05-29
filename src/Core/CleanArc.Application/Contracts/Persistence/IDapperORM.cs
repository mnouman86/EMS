using Dapper;

namespace CleanArc.Application.Contracts.Persistence;

public interface IDapperORM
{
    Task ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);
    Task<IEnumerable<T>> ReturnList<T>(string procedureName, DynamicParameters param = null);
    Task<(List<T1>, List<T2>)> ReturnMultipleList<T1, T2>(string procedureName, DynamicParameters param = null);
    Task<(List<T1>, List<T2>, List<T3>)> ReturnMultipleList<T1, T2, T3>(string procedureName, DynamicParameters param = null);
    Task<T> ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null);
    Task<IEnumerable<T>> ReturnPK<T>(string procedureName, DynamicParameters param = null);
}
