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

namespace BookingSundorbon.Features.Repositories.WeigthRepository
{
    internal class WeightRepository : IWeightRepository
    {
        private readonly string _connectionString;

        public WeightRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CreateWeightAsync(CreateWeigthView weigth)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", weigth.CompanyId, DbType.Int32);
                    parameters.Add("@WeightDescription", weigth.WeightDescription, DbType.String);
                    parameters.Add("@MinimumWeight", weigth.MinimumWeight, DbType.Decimal);
                    parameters.Add("@MaximumWeight", weigth.MaximumWeight, DbType.Decimal);
                    parameters.Add("@MeasurementUnitId", weigth.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Cost", weigth.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", weigth.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", weigth.CreatorId, DbType.String);

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
    }
}
