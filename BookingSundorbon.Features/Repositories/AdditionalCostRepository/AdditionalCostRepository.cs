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

        public async Task<int> CreateAdditionalCostAsync(AdditionalCostView AdditionalCost)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@AdditionalCostName", AdditionalCost.AdditionalCostName, DbType.String);
                    parameters.Add("@Cost", AdditionalCost.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", AdditionalCost.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", AdditionalCost.CreatorId, DbType.String);

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

                    var AdditionalCost = await dbConnection.QueryFirstOrDefaultAsync<AdditionalCostView>(
                        "[dbo].[SP_GetAdditionalCostDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return AdditionalCost;
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
                    var AdditionalCostes = await dbConnection.QueryAsync<AdditionalCostView>(
                        "[dbo].[SP_GetAllAdditionalCostes]", commandType: CommandType.StoredProcedure);

                    return AdditionalCostes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAdditionalCostAsync(AdditionalCostView AdditionalCost)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", AdditionalCost.Id, DbType.Int32);
                    parameters.Add("@Cost", AdditionalCost.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", AdditionalCost.IsActive, DbType.Boolean);
                    parameters.Add("@IsActive", AdditionalCost.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", AdditionalCost.ModifierId, DbType.String);

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
