using BookingSundorbon.Views.DTOs.AgentRequisitionView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Net;
using BookingSundorbon.Views.DTOs.AgentView;

namespace BookingSundorbon.Features.Repositories.AgentRequisitionRepository
{
    internal class AgentRequisitionRepository : IAgentRequisitionRepository
    {
        private readonly string _connectionString;

        public AgentRequisitionRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAgentRequisitionAsync(AgentRequisitionView agentRequisition)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();

                    parameters.Add("@requisitionDate", agentRequisition.RequisitionDate, DbType.DateTime);
                    parameters.Add("@agentId", agentRequisition.AgentId, DbType.Int32);
                    parameters.Add("@CreatorId", agentRequisition.CreatorId, DbType.String);


                    await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAgentRequisition]", parameters, commandType: CommandType.StoredProcedure);


                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }





    }
}
