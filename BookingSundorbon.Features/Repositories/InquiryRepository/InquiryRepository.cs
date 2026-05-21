using BookingSundorbon.Views.DTOs.InquiryView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.InquiryRepository
{
    internal class InquiryRepository:IInquiryRepository
    {
        private readonly string _connectionString;

        public InquiryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task InsertInquiryAsync(InquiryView inquiry)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Name", inquiry.Name, DbType.String);
                    parameters.Add("@Email", inquiry.Email, DbType.String);
                    parameters.Add("@Mobile", inquiry.Mobile, DbType.String);
                    parameters.Add("@Remarks", inquiry.Remarks, DbType.String);

                    inquiry.Id = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[sp_InsertInquiry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<InquiryView> GetInquiryByIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    return await dbConnection.QueryFirstOrDefaultAsync<InquiryView>(
                        "[dbo].[sp_GetInquiry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<InquiryView>> GetInquiriesAsync(string status = null, int page = 1, int pageSize = 20)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Status", status, DbType.String);
                    parameters.Add("@Page", page, DbType.Int32);
                    parameters.Add("@PageSize", pageSize, DbType.Int32);

                    return await dbConnection.QueryAsync<InquiryView>(
                        "[dbo].[sp_GetInquiry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateInquiryAsync(InquiryView inquiry)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", inquiry.Id, DbType.Int32);
                    parameters.Add("@Status", inquiry.Status, DbType.String);
                    parameters.Add("@Remarks", inquiry.Remarks, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[sp_UpdateInquiry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
