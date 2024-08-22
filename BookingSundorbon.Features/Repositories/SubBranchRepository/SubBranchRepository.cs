using BookingSundorbon.Views.DTOs.SubBranchView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SubBranchRepository
{
    internal class SubBranchRepository : ISubBranchRepository
    {
        private readonly string _connectionString;

        public SubBranchRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateSubBranchAsync(SubBranchCreateView subBranch)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                
                    DynamicParameters parameters = new();
                    parameters.Add("@SubBranchName", subBranch.SubBranchName, DbType.String);
                    parameters.Add("@IsHub", subBranch.IsHub, DbType.Boolean);
                    parameters.Add("@IsOffice", subBranch.IsOffice, DbType.Boolean);
                    parameters.Add("@IsAgent", subBranch.IsAgent, DbType.Boolean);
                    parameters.Add("@CountryId", subBranch.CountryId, DbType.Int32);
                    parameters.Add("@CityId", subBranch.CityId, DbType.Int32);
                    parameters.Add("@Address", subBranch.Address, DbType.String);
                    parameters.Add("@IsActive", subBranch.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", subBranch.CreatorId, DbType.String);

                    int newId = -1; 

                    if (subBranch.AgentId.Count > 0)
                    {
                        foreach (var agentId in subBranch.AgentId)
                        {
                            parameters.Add("@AgentId", agentId, DbType.Int32);
                            parameters.Add("@EmployeId", 0 , DbType.Int32); 
                            newId = await dbConnection.ExecuteScalarAsync<int>(
                                "[dbo].[SP_InsertIntoSubBranch]", parameters, commandType: CommandType.StoredProcedure);
                            
                        }
                    }
                    else if (subBranch.EmployeId.Count > 0)
                    {
                        foreach (var employeeId in subBranch.EmployeId)
                        {
                            parameters.Add("@EmployeId", employeeId, DbType.Int32);
                            parameters.Add("@AgentId", 0 , DbType.Int32); 
                            newId = await dbConnection.ExecuteScalarAsync<int>(
                                "[dbo].[SP_InsertIntoSubBranch]", parameters, commandType: CommandType.StoredProcedure);
                         
                        }
                    }

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<SubBranchView> GetSubBranchAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var subBranch = await dbConnection.QueryFirstOrDefaultAsync<SubBranchView>(
                        "[dbo].[SP_GetSubBranchDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return subBranch;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SubBranchView>> GetAllActiveSubBranchsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var subBranches = await dbConnection.QueryAsync<SubBranchView>(
                        "[dbo].[SP_GetAllActiveSubBranchs]", commandType: CommandType.StoredProcedure);

                    return subBranches;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateSubBranchAsync(SubBranchView subBranch)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", subBranch.Id, DbType.Int32);
                    parameters.Add("@SubBranchName", subBranch.SubBranchName, DbType.String);
                    parameters.Add("@IsHub", subBranch.IsHub, DbType.Boolean);
                    parameters.Add("@IsOffice", subBranch.IsOffice, DbType.Boolean);
                    parameters.Add("@AgentId", subBranch.AgentId, DbType.Int32);
                    parameters.Add("@IsAgent", subBranch.IsAgent, DbType.Boolean);
                    parameters.Add("@EmployeId", subBranch.EmployeId, DbType.Int32);
                    parameters.Add("@CountryId", subBranch.CountryId, DbType.Int32);
                    parameters.Add("@CityId", subBranch.CityId, DbType.Int32);
                    parameters.Add("@Address", subBranch.Address, DbType.String);
                    parameters.Add("@IsActive", subBranch.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", subBranch.ModifierId, DbType.String);
                 

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateSubBranch]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteSubBranchAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteSubBranch]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
