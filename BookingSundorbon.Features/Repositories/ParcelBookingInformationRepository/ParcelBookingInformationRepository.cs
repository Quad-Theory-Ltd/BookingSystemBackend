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

namespace BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository
{
    internal class ParcelBookingInformationRepository : IParcelBookingInformationRepository
    {
        private readonly string _connectionString;

        public ParcelBookingInformationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelInfoByUserIdAsync(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.Int32);

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

        public async Task<IEnumerable<ParcelBookingHistoryView>> GetParcelAgentBookingHistoryByAgentId(int AgentId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    
                  DynamicParameters parameters = new();
                    parameters.Add("@AgentUserId", AgentId, DbType.Int32);

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
    }
}
