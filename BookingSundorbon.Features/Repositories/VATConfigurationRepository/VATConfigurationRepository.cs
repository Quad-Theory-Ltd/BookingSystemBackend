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

        public async Task<int> CreateVatConfigurationAsync(VATConfigurationView vatConfiguration)
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

        public async Task DeleteVATConfigurationAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteVATConfiguration]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VATConfigurationView>> GetAllActiveVATConfigurationesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<VATConfigurationView>("SP_GetAllActiveVATConfiguration");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<VATConfigurationView> GetVATConfigurationAsync(int id)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var vatConfiguration = await dbConnection.QueryFirstOrDefaultAsync<VATConfigurationView>(
                        "[dbo].[SP_GetVATConfigurationDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return vatConfiguration;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateVATConfigurationAsync(VATConfigurationView vatconfiguration)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", vatconfiguration.Id, DbType.Int32);
                    parameters.Add("@ConfigurationDescription", vatconfiguration.ConfigurationDescription, DbType.String);
                    parameters.Add("@ConfigurationPercentage", vatconfiguration.ConfigurationPercentage, DbType.Decimal);
                    parameters.Add("@ConfigurationAmount", vatconfiguration.ConfigurationAmount, DbType.Decimal);
                    parameters.Add("@IsActive", vatconfiguration.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", vatconfiguration.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateVATconfiguration]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
