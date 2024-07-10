using BookingSundorbon.Views.DTOs.RoleView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingSundorbon.Features.Repositories.RoleRepository
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task CreateRoleAsynce(RoleView role)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", role.Id, DbType.String);
                    parameters.Add("@RoleName", role.RoleName, DbType.String);
                    parameters.Add("@IsActive", role.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", role.CreatorId, DbType.String);
                    parameters.Add("@IsDefault", role.IsDefault, DbType.Boolean);

                    await dbConnection.ExecuteScalarAsync<int>("[dbo].[SP_InsertIntoRole]", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex) {

                throw;
            }
        }

        public async Task DeleteRoleAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteRole]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<RoleView>> GetAllActiveRolesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var roles = await dbConnection.QueryAsync<RoleView> (
                        "[dbo].[SP_GetAllActiveRoles]", commandType: CommandType.StoredProcedure);

                    return roles;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<RoleView> GetRoleAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    var role = await dbConnection.QueryFirstOrDefaultAsync<RoleView>(
                        "[dbo].[SP_GetRoleDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return role;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateRoleAsync(RoleView role)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", role.Id, DbType.String);
                    parameters.Add("@RoleName", role.RoleName, DbType.String);
                    parameters.Add("@IsActive", role.IsActive, DbType.Boolean);
                    parameters.Add("@IsDefault", role.IsDefault, DbType.Boolean);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateRole]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    }

