using BookingSundorbon.Views.DTOs.S_UserView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.UserRepository
{
    internal class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

      

     

        public async Task<IEnumerable<EmployeeView>> GetAllEmployeeAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var devicees = await dbConnection.QueryAsync<EmployeeView>(
                        "[dbo].[SP_GetAllEmployee]", commandType: CommandType.StoredProcedure);

                    return devicees;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
