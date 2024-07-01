using BookingSundorbon.Views.DTOs.CountryView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookingSundorbon.Features.Repositories.CountryRepository
{
    internal class CountryRepository : ICountryRepository
    {
        private readonly string _connectionString;

        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<ActiveCountryView>> GetAllActiveCountriesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveCountryView>("SP_GetAllActiveCountries");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
