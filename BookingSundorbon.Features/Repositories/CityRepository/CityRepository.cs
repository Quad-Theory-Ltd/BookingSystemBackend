using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ActiveCityView;


namespace BookingSundorbon.Features.Repositories.CityRepository
{
    internal class CityRepository:ICityRepository
    {
        private readonly string _connectionString;

        public CityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<int> CreateCityAsync(ActiveCityView city)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", city.CompanyId, DbType.Int32);
                    parameters.Add("@Name", city.Name, DbType.String);
                    parameters.Add("@IsActive", city.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", city.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoCity]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActiveCityView> GetCityAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var city = await dbConnection.QueryFirstOrDefaultAsync<ActiveCityView>(
                        "[dbo].[SP_GetCityDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return city;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActiveCityView>> GetAllActiveCitiesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var cities = await dbConnection.QueryAsync<ActiveCityView>(
                        "[dbo].[SP_GetAllActiveCities]", commandType: CommandType.StoredProcedure);

                    return cities;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCityAsync(ActiveCityView city)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", city.Id, DbType.Int32);
                    parameters.Add("@CompanyId", city.CompanyId, DbType.Int32);
                    parameters.Add("@Name", city.Name, DbType.String);
                    parameters.Add("@IsActive", city.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", city.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateCity]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCityAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteCity]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
