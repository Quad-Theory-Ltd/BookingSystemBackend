using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.BranchView;

namespace BookingSundorbon.Features.Repositories.BranchRepository
{
    internal class BranchRepository:IBranchRepository
    {
        private readonly string _connectionString;

        public BranchRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateBranchAsync(BranchView branch)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@BranchName", branch.BranchName, DbType.String);
                    parameters.Add("@AddressLine", branch.AddressLine, DbType.String);
                    parameters.Add("@CompanyId", branch.CompanyId, DbType.Int32);
                    parameters.Add("@IsActive", branch.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", branch.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoBranch]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BranchView> GetBranchAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var branch = await dbConnection.QueryFirstOrDefaultAsync<BranchView>(
                        "[dbo].[SP_GetBranchDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return branch;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BranchView>> GetAllBranchesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var branches = await dbConnection.QueryAsync<BranchView>(
                        "[dbo].[SP_GetAllBranches]", commandType: CommandType.StoredProcedure);

                    return branches;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateBranchAsync(BranchView branch)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", branch.Id, DbType.Int32);
                    parameters.Add("@BranchName", branch.BranchName, DbType.String);
                    parameters.Add("@AddressLine", branch.AddressLine, DbType.String);
                    parameters.Add("@CompanyId", branch.CompanyId, DbType.Int32);
                    parameters.Add("@IsActive", branch.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", branch.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateBranch]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBranchAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteBranch]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
