using BookingSundorbon.Views.DTOs.CountryView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.CurrencyView;
using Dapper;

namespace BookingSundorbon.Features.Repositories.CurrencyRepository
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly string _connectionString;

        public CurrencyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<CurrencyView>> GetAllActiveCurrencyAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<CurrencyView>("Sp_GetAllActiveCurrency");

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<IEnumerable<CurrencyExchangeRateView>> GetAllCurrencyExchangeRateAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<CurrencyExchangeRateView>("Sp_GetAllCurrencyRate");

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
