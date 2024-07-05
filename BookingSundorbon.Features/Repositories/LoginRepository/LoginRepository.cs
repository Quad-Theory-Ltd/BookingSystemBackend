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
        private readonly string _connectionstring;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<LoginView> GetLoginByIdAsync(string userName, string password, string userType)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionstring))
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
    }
}
