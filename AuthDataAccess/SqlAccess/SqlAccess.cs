using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.SqlAccess;

public class SqlAccess : ISqlAccess
{
    private readonly IConfiguration _configuration;

    public SqlAccess()
    {
    }

    public SqlAccess(IConfiguration configuration)
    {
        _configuration = configuration;


    }

    public async Task<IEnumerable<T>> LoadAsync<T, U>(string storedProcedure, U parameters)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<T?> LoadSingleAsync<T, U>(string storedProcedure, U parameters)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task UpdateAsync<U>(string storedProcedures, U parameters)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            await connection.ExecuteAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<string> UpdateAsyncWithReturning<U>(string storedProcedure, U parameters)
    {
        var connectionString = _configuration.GetConnectionString("Default");
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);

            await connection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            return dynamicParameters.Get<string>("Result");
        }
    }

}
