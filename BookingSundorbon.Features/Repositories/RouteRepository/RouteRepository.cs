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

        public async Task<int> CreateRouteTypeAsync(CreateRouteTypeView routeType)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", routeType.CompanyId, DbType.Int32);
                    parameters.Add("@RouteName", routeType.RouteName, DbType.String);
                    parameters.Add("@StartingArea", routeType.StartingArea, DbType.String);
                    parameters.Add("@EndingArea", routeType.EndingArea, DbType.String);
                    parameters.Add("@RouteCost", routeType.RouteCost, DbType.Decimal);
                    parameters.Add("@IsActive", routeType.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", routeType.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoRoutingType]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
