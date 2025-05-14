using BookingSundorbon.Views.DTOs.LoginView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.LoginRepository
{
    internal class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<LoginView> GetLoginByIdAsync(string userName, string password, string userType)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserName", userName, DbType.String);
                    parameters.Add("@Password", password, DbType.String);
                    parameters.Add("@UserType", userType, DbType.String);


                    var results = await _dbConnection.QueryFirstOrDefaultAsync<LoginView>("[dbo].[SP_GetLoginById]", parameters, commandType: CommandType.StoredProcedure);

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoginView> GetLoginByUserIdAsync(int id)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);
                  
                    var results = await _dbConnection.QueryFirstOrDefaultAsync<LoginView>("[dbo].[SP_GetLoginByUserId]", parameters, commandType: CommandType.StoredProcedure);

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> CreateLoginAsync(LoginView login)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", login.UserId, DbType.String);
                    parameters.Add("@Password", login.Password, DbType.String);
                    parameters.Add("@UserType", login.UserType, DbType.String);
                    parameters.Add("@ParcelId", login.ParcelId, DbType.Int32);
                    parameters.Add("@AgentId", login.AgentId, DbType.Int32);
                    parameters.Add("@BranchId", login.BranchId, DbType.Int32);

                    var newId =  await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoLogin]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateLoginAsync(LoginView login)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", login.Id, DbType.Int32);
                    parameters.Add("@UserId", login.UserId, DbType.String);
                    parameters.Add("@Password", login.Password, DbType.String);
                    parameters.Add("@UserType", login.UserType, DbType.String);
                    parameters.Add("@ParcelId", login.ParcelId, DbType.Int32);
                    parameters.Add("@AgentId", login.AgentId, DbType.Int32);
                    parameters.Add("@BranchId", login.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateLogin]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
