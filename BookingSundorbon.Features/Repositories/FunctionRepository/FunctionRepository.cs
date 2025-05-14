using BookingSundorbon.Views.DTOs.FunctionView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Net;

namespace BookingSundorbon.Features.Repositories.FunctionRepository
{
    internal class FunctionRepository : IFunctionRepository
    {
        private readonly string _connectionString;

        public FunctionRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateFunctionAsync(FunctionView function)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", function.Id, DbType.String);
                    parameters.Add("@FunctionName", function.FunctionName, DbType.String);                    
                    parameters.Add("@IsActive", function.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", function.CreatorId, DbType.String);
                    parameters.Add("@BranchId", function.BranchId, DbType.Int32);
   
                    await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoFunction]", parameters, commandType: CommandType.StoredProcedure);
                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FunctionView> GetFunctionAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    var agent = await dbConnection.QueryFirstOrDefaultAsync<FunctionView>(
                        "[dbo].[SP_GetFunctionDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return agent;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FunctionView>> GetAllActiveFunctionAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var Functiones = await dbConnection.QueryAsync<FunctionView>(
                        "[dbo].[SP_GetAllActiveFunction]", commandType: CommandType.StoredProcedure);

                    return Functiones;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateFunctionAsync(FunctionView function)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", function.Id, DbType.String);
                    parameters.Add("@FunctionName", function.FunctionName, DbType.String);
                    parameters.Add("@IsActive", function.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", function.ModifierId, DbType.String);
                    parameters.Add("@BranchId", function.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateFunction]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteFunctionAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteFunction]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
