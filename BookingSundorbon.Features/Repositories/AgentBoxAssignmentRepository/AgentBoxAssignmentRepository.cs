using BookingSundorbon.Features.Repositories.AgentRepository;
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
using BookingSundorbon.Views.DTOs.AgentBoxAssignmentView;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignmentRepository
{
    internal class AgentBoxAssignmentRepository : IAgentBoxAssignmentRepository
    {
        private readonly string _connectionString;

        public AgentBoxAssignmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAgentBoxAssignmentAsync(AgentBoxAssignmentView agentBoxAssignment)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters agentId = new();

                    agentId.Add("@AgentId", agentBoxAssignment.AgentId, DbType.String);
                    var agent = await dbConnection.ExecuteScalarAsync<string>(
                       "[dbo].[SP_GetAgentNameById]", agentId, commandType: CommandType.StoredProcedure);

                    DynamicParameters parameters = new();
                    string boxSerialNo = $"{agentBoxAssignment.DimensionId}~{agent}~{Guid.NewGuid()}";

                    parameters.Add("@AgentId", agentBoxAssignment.AgentId, DbType.Int32);
                    parameters.Add("@DimensionId", agentBoxAssignment.DimensionId, DbType.Int32);
                    parameters.Add("@BoxSerialNo", boxSerialNo, DbType.String);
                    parameters.Add("@BoxQty", agentBoxAssignment.BoxQty, DbType.Int32);
                    parameters.Add("@IsActive", agentBoxAssignment.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", agentBoxAssignment.CreatorId, DbType.String);
                    parameters.Add("@CreationDate", agentBoxAssignment.CreationDate, DbType.DateTime);
                    parameters.Add("@ModifierId", agentBoxAssignment.ModifierId, DbType.String);
                    parameters.Add("@ModificationDate", agentBoxAssignment.ModificationDate, DbType.DateTime);

                    await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAgentAssignment]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<AgentBoxAssignmentView>> GetAllAgentBoxAssignment()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var agentBoxAssignments = await dbConnection.QueryAsync<AgentBoxAssignmentView>(
                        "[dbo].[SP_GetAllAgentBoxAssignment]", commandType: CommandType.StoredProcedure);

                    return agentBoxAssignments;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
