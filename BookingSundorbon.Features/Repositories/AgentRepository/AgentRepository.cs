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
using System.ComponentModel.Design;
using System.Net;

namespace BookingSundorbon.Features.Repositories.AgentRepository
{
    internal class AgentRepository : IAgentRepository
    {
        private readonly string _connectionString;

        public AgentRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAgentAsync(AgentView agent)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
    
                    parameters.Add("@CompanyId", agent.CompanyId, DbType.Int32);
                    parameters.Add("@Name", agent.Name, DbType.String);
                    parameters.Add("@Address", agent.Address, DbType.String);
                    parameters.Add("@Email", agent.Email, DbType.String);
                    parameters.Add("@MobileNo", agent.MobileNo, DbType.String);
                    parameters.Add("@TIN", agent.TIN, DbType.String);
                    parameters.Add("@BIN", agent.BIN, DbType.String);
                    parameters.Add("@BankAccountInfo", agent.BankAccountInfo, DbType.String);
                    parameters.Add("@ComissionPercentage", agent.ComissionPercentage, DbType.Decimal);
                    parameters.Add("@FixedCommisionAmount", agent.FixedCommisionAmount, DbType.Decimal);
                    parameters.Add("@IsActive", agent.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", agent.CreatorId, DbType.String);
                    parameters.Add("@BranchId", agent.BranchId, DbType.Int32);
                    parameters.Add("@UserId", agent.UserId, DbType.Int32);
                    parameters.Add("@SubBranchId", agent.SubBranchId, DbType.Int32);


                    await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAgent]", parameters, commandType: CommandType.StoredProcedure);

                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AgentView> GetAgentAsync(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.Int32);

                    var agent = await dbConnection.QueryFirstOrDefaultAsync<AgentView>(
                        "[dbo].[SP_GetAgentDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return agent;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AgentView>> GetAllActiveAgentAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var Agentes = await dbConnection.QueryAsync<AgentView>(
                        "[dbo].[SP_GetAllActiveAgent]", commandType: CommandType.StoredProcedure);

                    return Agentes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAgentAsync(AgentView agent)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
   
                    parameters.Add("@CompanyId", agent.CompanyId, DbType.Int32);
                    parameters.Add("@Name", agent.Name, DbType.String);
                    parameters.Add("@Address", agent.Address, DbType.String);
                    parameters.Add("@Email", agent.Email, DbType.String);
                    parameters.Add("@MobileNo", agent.MobileNo, DbType.String);
                    parameters.Add("@TIN", agent.TIN, DbType.String);
                    parameters.Add("@BIN", agent.BIN, DbType.String);
                    parameters.Add("@BankAccountInfo", agent.BankAccountInfo, DbType.String);
                    parameters.Add("@ComissionPercentage", agent.ComissionPercentage, DbType.Decimal);
                    parameters.Add("@FixedCommisionAmount", agent.FixedCommisionAmount, DbType.Decimal);
                    parameters.Add("@IsActive", agent.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", agent.ModifierId, DbType.String);
                    parameters.Add("@BranchId", agent.BranchId, DbType.Int32);
                    parameters.Add("@UserId", agent.UserId, DbType.Int32);
                    parameters.Add("@SubBranchId", agent.SubBranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateAgent]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAgentAsync(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteAgent]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
