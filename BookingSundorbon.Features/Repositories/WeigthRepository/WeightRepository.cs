using BookingSundorbon.Views.DTOs.WeightView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Features.Repositories.WeigthRepository;

namespace BookingSundorbon.Features.Repositories.WeightRepository
{
    internal class WeightRepository : IWeightRepository
    {
        private readonly string _connectionString;

        public WeightRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CreateWeightAsync(WeightView weight)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", weight.CompanyId, DbType.Int32);
                    parameters.Add("@WeightDescription", weight.WeightDescription, DbType.String);
                    parameters.Add("@MinimumWeight", weight.MinimumWeight, DbType.Decimal);
                    parameters.Add("@MaximumWeight", weight.MaximumWeight, DbType.Decimal);
                    parameters.Add("@MeasurementUnitId", weight.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Cost", weight.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", weight.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", weight.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoWeight]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteWeightAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteWeight]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<WeightView>> GetAllActiveWeightsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<WeightView>("SP_GetAllActiveWeights");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<WeightView> GetWeightAsync(int id)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var weight = await dbConnection.QueryFirstOrDefaultAsync<WeightView>(
                        "[dbo].[SP_GetWeightDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return weight;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateWeightAsync(WeightView weight)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", weight.Id, DbType.Int32);
                    parameters.Add("@CompanyId", weight.CompanyId, DbType.Int32);
                    parameters.Add("@WeightDescription", weight.WeightDescription, DbType.String);
                    parameters.Add("@MinimumWeight", weight.MinimumWeight, DbType.Decimal);
                    parameters.Add("@MaximumWeight", weight.MaximumWeight, DbType.Decimal);
                    parameters.Add("@MeasurementUnitId", weight.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Cost", weight.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", weight.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", weight.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateWeight]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
