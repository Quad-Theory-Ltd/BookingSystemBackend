using BookingSundorbon.Views.DTOs.DiscountedOfferDetailView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DiscountedOfferDetailRepository
{
    internal class DiscountedOfferDetailRepository : IDiscountedOfferDetailRepository
    {
        private readonly string _connectionString;

        public DiscountedOfferDetailRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDiscountedOfferDetailAsync(DiscountedOfferDetailView discountedOfferDetail)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();                  
                    parameters.Add("@DiscuntedOfferID", discountedOfferDetail.DiscuntedOfferID, DbType.Int32);
                    parameters.Add("@DiscountedDate", discountedOfferDetail.DiscountedDate, DbType.Date);
                    parameters.Add("@DiscountedPercentage", discountedOfferDetail.DiscountedPercentage, DbType.Decimal);
                    parameters.Add("@DiscountedAmount", discountedOfferDetail.DiscountedAmount, DbType.Decimal);
                    parameters.Add("@BranchId", discountedOfferDetail.BranchId, DbType.Int32);
                   

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDiscountedOfferDetail]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DiscountedOfferDetailView> GetDiscountedOfferDetailAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var discountedOfferDetail = await dbConnection.QueryFirstOrDefaultAsync<DiscountedOfferDetailView>(
                        "[dbo].[SP_GetDiscountedOfferDetailById_2]", parameters, commandType: CommandType.StoredProcedure);

                    return discountedOfferDetail;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DiscountedOfferDetailView>> GetAllDiscountedOfferDetailsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var discountedOfferDetailes = await dbConnection.QueryAsync<DiscountedOfferDetailView>(
                        "[dbo].[SP_GetAllDiscountedOfferDetails]", commandType: CommandType.StoredProcedure);

                    return discountedOfferDetailes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDiscountedOfferDetailAsync(DiscountedOfferDetailView discountedOfferDetail)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", discountedOfferDetail.Id, DbType.Int32);
                    parameters.Add("@DiscuntedOfferID", discountedOfferDetail.DiscuntedOfferID, DbType.Int32);
                    parameters.Add("@DiscountedDate", discountedOfferDetail.DiscountedDate, DbType.Date);
                    parameters.Add("@DiscountedPercentage", discountedOfferDetail.DiscountedPercentage, DbType.Decimal);
                    parameters.Add("@DiscountedAmount", discountedOfferDetail.DiscountedAmount, DbType.Decimal);
                    parameters.Add("@BranchId", discountedOfferDetail.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDiscountedOfferDetail]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDiscountedOfferDetailAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDiscountedOfferDetail]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
