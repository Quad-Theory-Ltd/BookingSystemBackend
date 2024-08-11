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

        public async Task<int> CreateRouteTypeAsync(RouteView routeType)
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
                    parameters.Add("@BranchId", routeType.BranchId, DbType.Int32);

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

        public async Task DeleteRouteAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteRoutingType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RouteView>> GetAllActiveRoutesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<RouteView>("SP_GetAllActiveRoutes");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<RouteView> GetRouteAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var route = await dbConnection.QueryFirstOrDefaultAsync<RouteView>(
                        "[dbo].[SP_GetRoutingTypeDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return route;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateRouteAsync(RouteView route)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", route.Id, DbType.Int32);
                    parameters.Add("@CompanyId", route.CompanyId, DbType.Int32);
                    parameters.Add("@RouteName", route.RouteName, DbType.String);
                    parameters.Add("@StartingArea", route.StartingArea, DbType.String);
                    parameters.Add("@EndingArea", route.EndingArea, DbType.String);
                    parameters.Add("@RouteCost", route.RouteCost, DbType.Decimal);
                    parameters.Add("@IsActive", route.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", route.ModifierId, DbType.String);
                    parameters.Add("@BranchId", route.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateRoutingType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
