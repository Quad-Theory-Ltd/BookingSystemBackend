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
 
        public async Task<int> CreateCountryAsync(ActiveCountryView country)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", country.Id, DbType.Int32);
                    parameters.Add("@CompanyId", country.CompanyId, DbType.Int32);
                    parameters.Add("@Name", country.Name, DbType.String);
                    parameters.Add("@IsActive", country.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", country.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoCountry]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActiveCountryView> GetCountryAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var country = await dbConnection.QueryFirstOrDefaultAsync<ActiveCountryView>(
                        "[dbo].[SP_GetCountryDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return country;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActiveCountryView>> GetAllActiveCountriesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveCountryView>("SP_GetAllActiveCountries");

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task UpdateCountryAsync(ActiveCountryView country)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", country.Id, DbType.Int32);
                    parameters.Add("@CompanyId", country.CompanyId, DbType.Int32);
                    parameters.Add("@Name", country.Name, DbType.String);
                    parameters.Add("@IsActive", country.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", country.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateCountry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCountryAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteCountry]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
