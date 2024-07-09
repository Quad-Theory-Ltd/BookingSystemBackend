using BookingSundorbon.Views.DTOs.ShippingService;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ShippingServiceRepository
{
    internal class ShippingServiceRepository : IShippingServiceRepository
    {
        private readonly string _connectionString;

        public ShippingServiceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateShippingServiceAsync(CreateShippingServiceView shippingService)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@RouteId", shippingService.RouteId, DbType.Int32);
                    parameters.Add("@CargoId ", shippingService.CargoId, DbType.Int32);
                    parameters.Add("@IsExpressService", shippingService.IsExpressService, DbType.Boolean);
                    parameters.Add("@ServiceName", shippingService.ServiceName, DbType.String);
                    parameters.Add("@Days", shippingService.Days, DbType.Int32);
                    parameters.Add("@ShippingServiceAmount", shippingService.ShippingServiceAmount, DbType.Decimal);
                    parameters.Add("@ShippingServiceAmountPercentage", shippingService.ShippingServiceAmountPercentage, DbType.Decimal);
                    parameters.Add("@IsActive", shippingService.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", shippingService.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoShippingService]", parameters, commandType: CommandType.StoredProcedure);

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
