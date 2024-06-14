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
        public async Task<List<ActiveCityView>> GetAllCitiesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveCityView>("SP_GetAllActiveCities");

                    return result.ToList();
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
    }
}
