using BookingSundorbon.Views.DTOs.RouteView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookingSundorbon.Features.Repositories.RouteRepository
{
    internal class RouteRepository : IRouteRepository
    {
        private readonly string _connectionString;

        public RouteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ActiveRouteView>> GetAllActiveRoutesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveRouteView> ("SP_GetAllActiveRoutes");

                    return result.ToList();
                }
            }
            catch (Exception ex) {

                throw;

            }
        }
    }
}
