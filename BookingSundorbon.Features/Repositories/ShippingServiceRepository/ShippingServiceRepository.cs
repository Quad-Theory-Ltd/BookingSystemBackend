using BookingSundorbon.Views.DTOs.ShippingServiceView;
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

        public async Task<int> CreateShippingServiceAsync(ShippingServiceView shippingService)
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

        public async Task DeleteShippingServiceAsync(int id)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteShippingService]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShippingServiceView>> GetAllActiveShippingServiceesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ShippingServiceView>("SP_GetAllActiveShippingService");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<ShippingServiceView> GetShippingServiceAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var shippingService = await dbConnection.QueryFirstOrDefaultAsync<ShippingServiceView>(
                        "[dbo].[SP_GetShippingServiceDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return shippingService;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateShippingServiceAsync(ShippingServiceView shippingService)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", shippingService.Id, DbType.Int32);
                    parameters.Add("@RouteId", shippingService.RouteId, DbType.Int32);
                    parameters.Add("@CargoId ", shippingService.CargoId, DbType.Int32);
                    parameters.Add("@IsExpressService", shippingService.IsExpressService, DbType.Boolean);
                    parameters.Add("@ServiceName", shippingService.ServiceName, DbType.String);
                    parameters.Add("@Days", shippingService.Days, DbType.Int32);
                    parameters.Add("@ShippingServiceAmount", shippingService.ShippingServiceAmount, DbType.Decimal);
                    parameters.Add("@ShippingServiceAmountPercentage", shippingService.ShippingServiceAmountPercentage, DbType.Decimal);
                    parameters.Add("@IsActive", shippingService.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", shippingService.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateShippingService]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
