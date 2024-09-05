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
using BookingSundorbon.Views.DTOs.AgentBoxAssignView;

namespace BookingSundorbon.Features.Repositories.AgentBoxAssignRepository
{
    internal class AgentBoxAssignRepository : IAgentBoxAssignRepository
    {
        private readonly string _connectionString;

        public AgentBoxAssignRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateAgentBoxAssignAsync(AgentBoxAssignView agentBoxAssign)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                  
                    DynamicParameters parameters = new();
                    string boxSerialNo = $"{agentBoxAssign.DimensionId}~{Guid.NewGuid()}";
                    parameters.Add("@AgentId", agentBoxAssign.AgentId, DbType.Int32);
                    parameters.Add("@DimensionId", agentBoxAssign.DimensionId, DbType.Int32);
                    parameters.Add("@BoxSerialNo", boxSerialNo, DbType.String);
                    parameters.Add("@BoxQty", agentBoxAssign.BoxQty);
                    parameters.Add("@SubBranchId", agentBoxAssign.SubBranchId, DbType.Int32);
                    parameters.Add("@IsActive", agentBoxAssign.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", agentBoxAssign.CreatorId, DbType.String);
                    

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAgentBoxAssign]", parameters, commandType: CommandType.StoredProcedure);


                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AgentBoxAssignView> GetAgentBoxAssignAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var agentBoxAssign = await dbConnection.QueryFirstOrDefaultAsync<AgentBoxAssignView>(
                        "[dbo].[SP_GetAgentBoxAssignDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return agentBoxAssign;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AgentBoxAssignView>> GetAllActiveAgentBoxAssignsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var agentBoxAssignes = await dbConnection.QueryAsync<AgentBoxAssignView>(
                        "[dbo].[SP_GetAllActiveAgentBoxAssigns]", commandType: CommandType.StoredProcedure);

                    return agentBoxAssignes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAgentBoxAssignAsync(AgentBoxAssignView agentBoxAssign)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", agentBoxAssign.Id, DbType.Int32);
                    parameters.Add("@AgentId", agentBoxAssign.AgentId, DbType.Int32);
                    parameters.Add("@DimensionId", agentBoxAssign.DimensionId, DbType.Int32);
                    parameters.Add("@BoxSerialNo", agentBoxAssign.BoxSerialNo, DbType.String);
                    parameters.Add("@BoxQty", agentBoxAssign.BoxQty);
                    parameters.Add("@SubBranchId", agentBoxAssign.SubBranchId, DbType.Int32);
                    parameters.Add("@IsActive", agentBoxAssign.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", agentBoxAssign.ModifierId, DbType.String);
                   

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateAgentBoxAssign]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task DeleteAgentBoxAssignAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteAgentBoxAssign]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<AgentBoxAssignView> AgentBoxAssignByDetailsByIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var agentBox = await dbConnection.QueryFirstOrDefaultAsync<AgentBoxAssignView>(
                        "[dbo].[SP_GetAgentBoxAssignDetailsById]", parameters, commandType: CommandType.StoredProcedure);

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
