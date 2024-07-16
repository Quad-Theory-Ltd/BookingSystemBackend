using BookingSundorbon.Views.DTOs.DiscountedOfferView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DiscountedOfferRepository
{
    internal class DiscountedOfferRepository : IDiscountedOfferRepository
    {
        private readonly string _connectionString;

        public DiscountedOfferRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDiscountedOfferAsync(DiscountedOfferView discountedOffer)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();                  
                    parameters.Add("@RouteId", discountedOffer.RouteId, DbType.Int32);
                    parameters.Add("@CargoId", discountedOffer.CargoId, DbType.Int32);
                    parameters.Add("@StartDate", discountedOffer.StartDate, DbType.DateTime);
                    parameters.Add("@EndDate", discountedOffer.EndDate, DbType.DateTime);
                    parameters.Add("@DiscontDescription", discountedOffer.DiscontDescription, DbType.String);
                    parameters.Add("@IsActive", discountedOffer.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", discountedOffer.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDiscountedOffer]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DiscountedOfferView> GetDiscountedOfferAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var discountedOffer = await dbConnection.QueryFirstOrDefaultAsync<DiscountedOfferView>(
                        "[dbo].[SP_GetDiscountedOfferDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return discountedOffer;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DiscountedOfferView>> GetAllActiveDiscountedOffersAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var discountedOfferes = await dbConnection.QueryAsync<DiscountedOfferView>(
                        "[dbo].[SP_GetAllActiveDiscountedOffers]", commandType: CommandType.StoredProcedure);

                    return discountedOfferes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDiscountedOfferAsync(DiscountedOfferView discountedOffer)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", discountedOffer.Id, DbType.Int32);
                    parameters.Add("@RouteId", discountedOffer.RouteId, DbType.Int32);
                    parameters.Add("@CargoId", discountedOffer.CargoId, DbType.Int32);
                    parameters.Add("@StartDate", discountedOffer.StartDate, DbType.DateTime);
                    parameters.Add("@EndDate", discountedOffer.EndDate, DbType.DateTime);
                    parameters.Add("@DiscontDescription", discountedOffer.DiscontDescription, DbType.String);
                    parameters.Add("@IsActive", discountedOffer.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", discountedOffer.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDiscountedOffer]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDiscountedOfferAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDiscountedOffer]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
