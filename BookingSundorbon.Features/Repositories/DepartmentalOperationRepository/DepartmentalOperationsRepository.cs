using BookingSundorbon.Views.DTOs.DepartmentalOperationView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DepartmentalOperationRepository
{
    internal class DepartmentalOperationRepository : IDepartmentalOperationRepository
    {
        private readonly string _connectionString;

        public DepartmentalOperationRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDepartmentalOperationAsync(DepartmentalOperationView departmentalOperation)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", departmentalOperation.CompanyId, DbType.Int32);
                    parameters.Add("@DepartmentId", departmentalOperation.DepartmentId, DbType.Int32);
                    parameters.Add("@OperationId", departmentalOperation.OperationId, DbType.Int32);
                    parameters.Add("@HasAccess", departmentalOperation.HasAccess, DbType.Boolean);
                    parameters.Add("@CreatorId", departmentalOperation.CreatorId, DbType.String);
                    parameters.Add("@BranchId", departmentalOperation.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDepartmentalOperation]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DepartmentalOperationView> GetDepartmentalOperationAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var departmentalOperation = await dbConnection.QueryFirstOrDefaultAsync<DepartmentalOperationView>(
                        "[dbo].[SP_GetDepartmentalOperationDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return departmentalOperation;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentalOperationView>> GetAllDepartmentalOperationAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var departmentalOperationes = await dbConnection.QueryAsync<DepartmentalOperationView>(
                        "[dbo].[SP_GetAllDepartmentalOperations]", commandType: CommandType.StoredProcedure);

                    return departmentalOperationes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDepartmentalOperationAsync(DepartmentalOperationView departmentalOperation)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", departmentalOperation.Id, DbType.Int32);
                    parameters.Add("@CompanyId", departmentalOperation.CompanyId, DbType.Int32);
                    parameters.Add("@DepartmentId", departmentalOperation.DepartmentId, DbType.Int32);
                    parameters.Add("@OperationId", departmentalOperation.OperationId, DbType.Int32);
                    parameters.Add("@HasAccess", departmentalOperation.HasAccess, DbType.Boolean);
                    parameters.Add("@ModifierId", departmentalOperation.ModifierId, DbType.String);
                    parameters.Add("@BranchId", departmentalOperation.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDepartmentalOperation]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDepartmentalOperationAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDepartmentalOperation]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
