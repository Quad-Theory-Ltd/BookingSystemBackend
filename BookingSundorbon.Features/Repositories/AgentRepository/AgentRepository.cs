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
using System.Security.Cryptography;
using BookingSundorbon.Views.DTOs.TransitionCostView;
using System.Text.Json;
using BookingSundorbon.Features.Helpers;

namespace BookingSundorbon.Features.Repositories.AgentRepository
{
    public class AgentRepository : IAgentRepository
    {
        private readonly string _connectionString;

        public AgentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> CreateAgentAsync(AgentView agent)
        {
            try
            {

                int userId = 0;

                var user = new
                {
                    Id = 0,
                    RoleId = 2,
                    UserName = agent.Name,
                    IsEmailConfirmed = false,
                    UserEmail = agent.Email,
                    IsTemporaryPass = true,
                    PasswordHash = agent.Password,
                    PhoneNo = agent.MobileNo,
                    Address = agent.Address,
                    IsActive = true,
                    CreatorId = agent.CreatorId,
                    CreationDate = DateTime.UtcNow,
                    ModifierId = agent.ModifierId,
                    ModificationDate = DateTime.UtcNow,
                    RoleName = ""
                };

                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri("https://localhost:7187");

                        //httpClient.BaseAddress = new Uri("https://bookingrolesandpermissions.azurewebsites.net");

                        string jsonData = JsonSerializer.Serialize(user);
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await httpClient.PostAsync("/api/UserLogin", content);

                        var httpResult = await response.Content.ReadAsStringAsync();
                        try
                        {
                            userId=int.Parse(httpResult);
                        }
                        catch (Exception ex)
                        {
                            return httpResult;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }





                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();

                    parameters.Add("@CompanyId", agent.CompanyId, DbType.Int32);
                    parameters.Add("@UserId", userId, DbType.Int32);
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
                    parameters.Add("@SubBranchId", agent.SubBranchId, DbType.Int32);
                    parameters.Add("@Password", agent.Password, DbType.String);


                    await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoAgent]", parameters, commandType: CommandType.StoredProcedure);



                    return "User Created";

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
                    parameters.Add("@UserId", agent.UserId, DbType.Int32);
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

        public Task<AgentView> GetAgentAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAgentAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
