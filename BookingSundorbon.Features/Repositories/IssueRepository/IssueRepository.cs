using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.IssueView;



namespace BookingSundorbon.Features.Repositories.IssueRepository
{
    internal class IssueRepository:IIssueRepository
    {
        private readonly string _connectionString;

        public IssueRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<int> CreateIssueAsync(IssueView issue)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@IssueDate", issue.IssueDate, DbType.DateTime);
                    parameters.Add ("@AgentRequisitionNo" , issue.AgentRequisitionNo, DbType.Int32);
                    parameters.Add("@CreatorId", issue.CreatorId, DbType.String);

                    parameters.Add("@IssuedBy", issue.IssuedBy, DbType.Int32);
                    parameters.Add("@IssuedPrice", issue.IssuedPrice, DbType.Decimal);
                    parameters.Add("@Remarks", issue.Remarks, DbType.String);
                    parameters.Add("@DimensionId", issue.DimensionId, DbType.Int32);
                    parameters.Add("@IssuedQty", issue.IssuedQty, DbType.Int32);
                    parameters.Add("@RecordSerialNo", issue.RecordSerialNo, DbType.String);



                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoIssue]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IssueView>> GetAllIssueAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var issue = await dbConnection.QueryAsync<IssueView>(
                        "[dbo].[SP_GetAllIssue]", commandType: CommandType.StoredProcedure);

                    return issue;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetAllIssueNo()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var issueNo = await dbConnection.QueryAsync<int>(
                        "[dbo].[SP_GetAllIssueNo]", commandType: CommandType.StoredProcedure);

                    return issueNo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IssueView> GetIssueAsync(int issueNo)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@issueNo", issueNo, DbType.Int32);

                    var issue = await dbConnection.QueryFirstOrDefaultAsync<IssueView>(
                        "[dbo].[SP_GetIssueDetailsByIssueNo]", parameters, commandType: CommandType.StoredProcedure);

                    return issue;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetNextIssueNoAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var requisitionNumber = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_GetLastIssueNo]", commandType: CommandType.StoredProcedure);

                    return requisitionNumber + 1;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
