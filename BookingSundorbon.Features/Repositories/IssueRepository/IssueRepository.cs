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

 

    }
}
