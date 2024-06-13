using BookingSundorbon.Views.DTOs.CityView;
using BookingSundorbon.Views.DTOs.DistrictView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookingSundorbon.Features.Repositories.CityRepository
{
    public class CityRepository : ICityRepository
    {
        private readonly string _connectionString;
        public CityRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
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

        public async Task<ActiveCityView> GetCityByIdAsync(int id, bool isActive)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@id", id, DbType.Int32);
                    parameters.Add("@isActive", isActive, DbType.Boolean);

                    var result = await dbConnection.QueryFirstOrDefaultAsync<ActiveCityView>("SP_GetCitiesById", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
