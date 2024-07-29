using BookingSundorbon.Views.DTOs.ScreenFunctionView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BookingSundorbon.Features.Repositories.ScreenFunctionRepository
{
    public class ScreenFunctionRepository : IScreenFunctionRepository
    {
        private readonly string _connectionString;

        public ScreenFunctionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


         public async Task CreateScreenFunctionAsync(ScreenFunctionView screenFunction)
         {
             try
             {
                 using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                 {
                     DynamicParameters parameters = new();
                     parameters.Add("@Id", screenFunction.Id, DbType.String);
                     parameters.Add("@ScreenId", screenFunction.ScreenId, DbType.String);
                     parameters.Add("@FunctionId", screenFunction.FunctionId, DbType.String);
                     parameters.Add("@IsActive", screenFunction.IsActive, DbType.Boolean);
                     parameters.Add("@CreatorId", screenFunction.CreatorId, DbType.String);

                     await dbConnection.ExecuteScalarAsync<int>("[dbo].[SP_InsertIntoScreenFunctions]", parameters, commandType: CommandType.StoredProcedure);

                 }
             }
             catch (Exception ex)
             {

                 throw;
             }
         }

        public async Task DeleteScreenFunctionAsync(string id)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteScreenFunction]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ScreenFunctionView>> GetAllActiveScreenesFunctionAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ScreenFunctionView>("SP_GetAllActiveScreenFunctions");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<ScreenFunctionView> GetScreenFunctionAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    var screenfunction = await dbConnection.QueryFirstOrDefaultAsync<ScreenFunctionView>(
                        "[dbo].[SP_GetScreenFunctionDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return screenfunction;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateScreenFunctionAsync(ScreenFunctionView screenFunction)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", screenFunction.Id, DbType.String);
                    parameters.Add("@ScreenId", screenFunction.ScreenId, DbType.String);
                    parameters.Add("@FunctionId", screenFunction.FunctionId, DbType.String);
                    parameters.Add("@IsActive", screenFunction.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", screenFunction.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateScreenFunction]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
