using BookingSundorbon.Views.DTOs.ApplicationUserView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;

namespace BookingSundorbon.Features.Repositories.ApplicationUserRepository
{
    internal class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly string _connectionString;

        public ApplicationUserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ApplicationDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' is not configured.");
        }

        public async Task<IEnumerable<AdminUserDetailView>> GetAdminUserDetailsAsync()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);

            return await dbConnection.QueryAsync<AdminUserDetailView>(
                "[dbo].[SP_GetAdminUserDetails]",
                commandType: CommandType.StoredProcedure);
        }
    }
}
