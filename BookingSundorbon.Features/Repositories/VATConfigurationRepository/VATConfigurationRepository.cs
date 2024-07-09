using BookingSundorbon.Views.DTOs.VATConfigurationView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.VATConfigurationRepository
{
    internal class VATConfigurationRepository : IVATConfigurationRepository
    {
        private readonly string _connectionString;
        public VATConfigurationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateVatConfigurationAsync(CreateVATConfigurationView vatConfiguration)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ConfigurationDescription", vatConfiguration.ConfigurationDescription, DbType.String);
                    parameters.Add("@ConfigurationPercentage", vatConfiguration.ConfigurationPercentage, DbType.Decimal);
                    parameters.Add("@ConfigurationAmount", vatConfiguration.ConfigurationAmount, DbType.Decimal);
                    parameters.Add("@IsActive", vatConfiguration.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", vatConfiguration.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoVATConfiguration]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
