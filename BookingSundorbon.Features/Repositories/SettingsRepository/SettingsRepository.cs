using BookingSundorbon.Views.DTOs.SettingsView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.SettingsRepository
{
    internal class SettingsRepository : ISettingsRepository
    {
        private readonly string _connectionString;
        public SettingsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<GtmSettingsView> GetGtmSettingsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryFirstOrDefaultAsync<GtmSettingsView>(
                        "[dbo].[SP_GetGtmSettings]", 
                        commandType: CommandType.StoredProcedure);

                    // If no record exists, return empty strings
                    if (result == null)
                    {
                        return new GtmSettingsView
                        {
                            GtmHeadCode = string.Empty,
                            GtmBodyCode = string.Empty
                        };
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                // If table doesn't exist yet, return empty settings
                return new GtmSettingsView
                {
                    GtmHeadCode = string.Empty,
                    GtmBodyCode = string.Empty
                };
            }
        }

        public async Task SaveGtmSettingsAsync(SaveGtmSettingsView settings)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@GtmHeadCode", settings.GtmHeadCode, DbType.String);
                    parameters.Add("@GtmBodyCode", settings.GtmBodyCode, DbType.String);
                    parameters.Add("@CreatorId", "System", DbType.String);
                    parameters.Add("@ModifierId", "System", DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_SaveGtmSettings]", 
                        parameters, 
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
