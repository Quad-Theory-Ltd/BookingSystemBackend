using BookingSundorbon.Views.DTOs.ParcelBookingInformationView;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BookingSundorbon.Views.DTOs.BranchView;
using BookingSundorbon.Views.DTOs.ParcelCountView;
using BookingSundorbon.Views.DTOs.ParcelBoxCountView;
using BookingSundorbon.Views.DTOs.ParcelBookingHistoryView;
using System.Data.Common;
using BookingSundorbon.Views.DTOs.BarcodeScanView;

namespace BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository
{
    internal class ParcelBookingInformationRepository : IParcelBookingInformationRepository
    {
        private readonly string _connectionString;

        public ParcelBookingInformationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelInfoByUserIdAsync(string userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.String);

                    var result = await dbConnection.QueryAsync<ParcelBookingHistoryView>(
                        "[dbo].[SP_GetAgentBookingDetailsByUserId]", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ParcelCountView>> GetParcelCounts()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ParcelCountView>(
                        "[dbo].[SP_GetParcelCounts]", commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ParcelBoxCountView>> GetParcelCountsWithDimensions()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ParcelBoxCountView>(
                        "[dbo].[SP_GetParcelCountsWithDimensions]", commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelBookingHistory()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ParcelBookingHistoryView>(
                        "[dbo].[SP_GetAllBookingHistory]", commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelAgentBookingHistory()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ParcelBookingHistoryView>(
                        "[dbo].[SP_GetAgentBookingHistory]", commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ParcelResponseDto>> GetParcelHistory()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                using var multi = await connection.QueryMultipleAsync(
                    "SP_GetParcelHistory",
                    commandType: CommandType.StoredProcedure
                );
                var headers = (await multi.ReadAsync<ParcelHeaderDto>())
                    .ToList();
                var details = await multi.ReadAsync<ParcelDetailDto>();
                var result = headers
                    .GroupBy(x => x.ParcelNo)
                    .Select(g =>
                    {
                        var h = g.First();

                        return new ParcelResponseDto
                        {
                            ParcelNo = h.ParcelNo,

                            ParcelStatusId = h.ParcelStatusId,
                            ParcelStatusName = h.ParcelStatusName,

                            SenderName = h.SenderName,
                            SenderEmail = h.SenderEmail,
                            SenderPhone = h.SenderPhone,
                            SenderPostCode = h.SenderPostCode,

                            ReceiverName = h.ReceiverName,
                            ReceiverEmail = h.ReceiverEmail,
                            ReceiverPhone = h.ReceiverPhone,
                            ReceiverPostCode = h.ReceiverPostCode,

                            CardBrand=h.CardBrand,
                            CardLast4=h.CardLast4,
                            StripeChargeId=h.StripeChargeId,
                            StripePaymentIntentId = h.StripePaymentIntentId,
                            StripePaymentMethod = h.StripePaymentMethod

                        };
                    })
                    .ToList();

                // attach details
                var lookup = result.ToDictionary(x => x.ParcelNo);

                foreach (var d in details)
                {
                    if (lookup.TryGetValue(d.ParcelNo, out var parcel))
                    {
                        parcel.Details.Add(d);
                    }
                }

                return result.OrderByDescending(x=>x.ParcelNo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelAgentBookingHistoryByAgentId(string AgentId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    
                  DynamicParameters parameters = new();
                    parameters.Add("@AgentUserId", AgentId, DbType.String);

                    var result = await dbConnection.QueryAsync<ParcelBookingHistoryView>(
                        "[dbo].[SP_GetAgentBookingDetailsByAgentId]", parameters,commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelBookingHistoryByUserIdAsync(string userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.String);

                    var result = await dbConnection.QueryAsync<ParcelBookingHistoryView>(
                        "[dbo].[SP_GetUserBookingDetailsByUserId]", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ParcelBookingSummaryCountsView>> GetUserBookingSummery(string userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.String);

                    var result = await dbConnection.QueryAsync<ParcelBookingSummaryCountsView>(
                        "[dbo].[Sp_GetUserBookingSummery]", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> GetAnalyticsData()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {

                    var result = await dbConnection.QueryFirstOrDefaultAsync<string>(
                        "[dbo].[sp_GetBookingSummary]", commandType: CommandType.StoredProcedure);

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
