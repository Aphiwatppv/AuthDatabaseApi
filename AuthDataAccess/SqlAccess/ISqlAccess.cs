
namespace AuthDataAccess.SqlAccess
{
    public interface ISqlAccess
    {
        Task<IEnumerable<T>> LoadAsync<T, U>(string storedProcedure, U parameters);
        Task<T> LoadSingleAsync<T, U>(string storedProcedure, U parameters);
        Task UpdateAsync<U>(string storedProcedures, U parameters);
        Task<string> UpdateAsyncWithReturning<U>(string storedProcedure, U parameters);
    }
}