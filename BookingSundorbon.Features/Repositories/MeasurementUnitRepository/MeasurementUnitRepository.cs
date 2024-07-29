using BookingSundorbon.Views.DTOs.MeasurementUnitView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.MeasurementUnitRepository
{
    internal class MeasurementUnitRepository : IMeasurementUnitRepository
    {
        private readonly string _connectionString;

        public MeasurementUnitRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CreateMeasurementUnitAsync(MeasurementUnitView measurementUnit)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UnitDescription", measurementUnit.UnitDescription, DbType.String);
                    parameters.Add("@IsActive", measurementUnit.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", measurementUnit.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoMeasurementUnit]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteMeasurementUnitAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteMeasurementUnit]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<MeasurementUnitView>> GetAllActiveMeasurementUnitsAsync()
        {
            try { 
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var measurementUnits = await dbConnection.QueryAsync<MeasurementUnitView>(
                    "[dbo].[SP_GetAllActiveMeasurementUnits]", commandType: CommandType.StoredProcedure);

                return measurementUnits;
            }
        }
            catch (Exception ex)
            {
                throw;
            }
}

        public async Task<MeasurementUnitView> GetMeasurementUnitAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var measurementUnit = await dbConnection.QueryFirstOrDefaultAsync<MeasurementUnitView>(
                        "[dbo].[SP_GetMeasurementUnitDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return measurementUnit;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateMeasurementUnitAsync(MeasurementUnitView measurementUnit)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", measurementUnit.Id, DbType.Int32);
                    parameters.Add("@UnitDescription", measurementUnit.UnitDescription, DbType.String);
                    parameters.Add("@IsActive", measurementUnit.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", measurementUnit.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateMeasurementUnit]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
