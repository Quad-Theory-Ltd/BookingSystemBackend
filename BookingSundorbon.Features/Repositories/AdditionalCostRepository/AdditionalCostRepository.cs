using BookingSundorbon.Views.DTOs.AdditionalCostView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AdditionalCostRepository
{
    internal class AdditionalCostRepository : IAdditionalCostRepository
    {
        private readonly string _connectionString;

        public AdditionalCostRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateAdditionalCostAsync(AdditionalCostView additionalCost)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@AdditionaCostName", additionalCost.AdditionaCostName, DbType.String);
                    parameters.Add("@Cost", additionalCost.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", additionalCost.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", additionalCost.CreatorId, DbType.String);
                    parameters.Add("@BranchId", additionalCost.BranchId, DbType.Int32);


                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAdditionalCost]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AdditionalCostView> GetAdditionalCostAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var additionalCost = await dbConnection.QueryFirstOrDefaultAsync<AdditionalCostView>(
                        "[dbo].[SP_GetAdditionalCostDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return additionalCost;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AdditionalCostView>> GetAllActiveAdditionalCostAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var additionalCostes = await dbConnection.QueryAsync<AdditionalCostView>(
                        "[dbo].[SP_GetAllActiveAdditionalCost]", commandType: CommandType.StoredProcedure);

                    return additionalCostes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAdditionalCostAsync(AdditionalCostView additionalCost)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", additionalCost.Id, DbType.Int32);
                    parameters.Add("@AdditionaCostName", additionalCost.AdditionaCostName, DbType.String);
                    parameters.Add("@Cost", additionalCost.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", additionalCost.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", additionalCost.ModifierId, DbType.String);
                    parameters.Add("@BranchId", additionalCost.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateAdditionalCost]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAdditionalCostAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteAdditionalCost]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
