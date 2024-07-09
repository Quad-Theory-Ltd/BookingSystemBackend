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

        public async Task CreateRoleAsynce(CreateRoleView role)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ID", role.Id, DbType.String);
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
           
}
    }

