using BookingSundorbon.Views.DTOs.AgentBoxAssignView;
using BookingSundorbon.Views.DTOs.AgentView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignRepository
{
    internal class AgentBoxAssignRepository : IAgentBoxAssignRepository
    {
        private readonly string _connectionString;

        public AgentBoxAssignRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<AgentBoxAssignView> AgentBoxAssignByDetailsByIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var agentBox = await dbConnection.QueryFirstOrDefaultAsync<AgentBoxAssignView>(
                        "[dbo].[SP_AgentBoxAssignDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return agentBox;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AgentBoxAssignDetailsView>> AgentBoxAssignDetailsByAgentIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@AgentId", id, DbType.Int32);

                    var agentCount = await dbConnection.QueryAsync<AgentBoxAssignDetailsView>(
                        "[dbo].[SP_AgentBoxAssignDetailsByAgentId]", parameters, commandType: CommandType.StoredProcedure);

                    return agentCount;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
