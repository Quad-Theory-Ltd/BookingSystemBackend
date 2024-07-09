using BookingSundorbon.Views.DTOs.AgentBookingView;
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
        public async Task<int> CreateMeasurementUnitAsync(CreateMeasurementUnitView measurementUnit)
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
    }
}
