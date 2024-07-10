using BookingSundorbon.Views.DTOs.PickupView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingSundorbon.Features.Repositories.PickupRepository
{
    internal class PickupRepository : IPickupRepository
    {
        private readonly string _connectionString;
        public PickupRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");            
        }
        public async Task<int> CreatePickupAsync(PickupView pickup)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@PickUpDescription", pickup.PickUpDescription, DbType.String);
                    parameters.Add("@Cost", pickup.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", pickup.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", pickup.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoPickup]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task DeletePickupAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeletePickup]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PickupView>> GetAllActivePickupUnitsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var pickup = await dbConnection.QueryAsync<PickupView>(
                        "[dbo].[SP_GetAllActivePickup]", commandType: CommandType.StoredProcedure);

                    return pickup;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PickupView> GetPickupAsync(int id)
        {
            try { 
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                DynamicParameters parameters = new();
                parameters.Add("@Id", id, DbType.Int32);

                var pickup = await dbConnection.QueryFirstOrDefaultAsync<PickupView>(
                    "[dbo].[SP_GetPickUpDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                return pickup;
            }
        }
            catch (Exception ex)
            {
                throw;
            }
}

        public async Task UpdatePickupAsync(PickupView pickup)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", pickup.Id, DbType.Int32);
                    parameters.Add("@PickUpDescription", pickup.PickUpDescription, DbType.String);
                    parameters.Add("@Cost", pickup.Cost, DbType.String);
                    parameters.Add("@IsActive", pickup.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", pickup.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdatePickup]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
