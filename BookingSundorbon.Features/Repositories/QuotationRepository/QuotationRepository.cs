using BookingSundorbon.Views.DTOs.QuotationView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.QuotationRepository
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly string _connectionString;

        public QuotationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<GetPricingResponseView> GetPricingAsync(GetPricingView getPricingView)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@RouteId", getPricingView.RouteId, DbType.Int32);
                    parameters.Add("@MeasurementUnitId", getPricingView.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Weight", getPricingView.Weight, DbType.Decimal);
                    parameters.Add("@Volume", getPricingView.Volume, DbType.Decimal);
                    parameters.Add("@Length", getPricingView.Length ?? (object)DBNull.Value, DbType.Decimal);
                    parameters.Add("@Width", getPricingView.Width ?? (object)DBNull.Value, DbType.Decimal);
                    parameters.Add("@Height", getPricingView.Height ?? (object)DBNull.Value, DbType.Decimal);
                    parameters.Add("@QuotationType", getPricingView.QuotationType ?? "Regular", DbType.String);

                    var result = await dbConnection.QueryFirstOrDefaultAsync<GetPricingResponseView>(
                        "[dbo].[sp_GetPricing]", parameters, commandType: CommandType.StoredProcedure);

                    return result ?? new GetPricingResponseView
                    {
                        IsSuccess = 0,
                        Message = "No pricing data found"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GetPricingResponseView
                {
                    IsSuccess = 0,
                    Message = $"Error getting pricing: {ex.Message}"
                };
            }
        }

        public async Task<GetPricingResponseView> GetMangoPricingAsync(MangoPricingView mangoPricingView)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@RouteId", mangoPricingView.RouteId, DbType.Int32);
                    parameters.Add("@WeightId", mangoPricingView.WeightId , DbType.Int32);

                    var result = await dbConnection.QueryFirstOrDefaultAsync<GetPricingResponseView>(
                        "sp_GetMangoPricing", parameters, commandType: CommandType.StoredProcedure);

                    return result ?? new GetPricingResponseView
                    {
                        IsSuccess = 0,
                        Message = "No data returned from stored procedure"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GetPricingResponseView
                {
                    IsSuccess = 0,
                    Message = $"Error getting mango pricing: {ex.Message}"
                };
            }
        }
    }
}

