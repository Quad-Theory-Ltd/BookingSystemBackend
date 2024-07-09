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
        public async Task<int> CreatePickupAsync(CreatePickupView pickup)
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
    }
}
