using BookingSundorbon.Views.DTOs.DimensionView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BookingSundorbon.Features.Repositories.DimensionRepository
{
    internal class DimensionRepository : IDimensionRepository
    {
        private readonly string _connectionString;

        public DimensionRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDimensionAsync(DimensionView dimension)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", dimension.CompanyId, DbType.Int32);
                    parameters.Add("@DimensionName", dimension.DimensionName, DbType.String);
                    parameters.Add("@Length", dimension.Length, DbType.Decimal);
                    parameters.Add("@Width", dimension.Width, DbType.Decimal);
                    parameters.Add("@Height", dimension.Height, DbType.Decimal);
                    parameters.Add("@MeasurementUnitId", dimension.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Price", dimension.Price, DbType.Decimal);
                    parameters.Add("@IsActive", dimension.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", dimension.CreatorId, DbType.String);
                    parameters.Add("@BranchId", dimension.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDimension]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DimensionView> GetDimensionAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var dimension = await dbConnection.QueryFirstOrDefaultAsync<DimensionView>(
                        "[dbo].[SP_GetDimensionDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return dimension;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DimensionView>> GetAllActiveDimensionsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var dimensiones = await dbConnection.QueryAsync<DimensionView>(
                        "[dbo].[SP_GetAllActiveDimensions]", commandType: CommandType.StoredProcedure);

                    return dimensiones;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDimensionAsync(DimensionView dimension)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", dimension.Id, DbType.Int32);
                    parameters.Add("@CompanyId", dimension.CompanyId, DbType.Int32);
                    parameters.Add("@DimensionName", dimension.DimensionName, DbType.String);
                    parameters.Add("@Length", dimension.Length, DbType.Decimal);
                    parameters.Add("@Width", dimension.Width, DbType.Decimal);
                    parameters.Add("@Height", dimension.Height, DbType.Decimal);
                    parameters.Add("@MeasurementUnitId", dimension.MeasurementUnitId, DbType.Int32);
                    parameters.Add("@Price", dimension.Price, DbType.Decimal);
                    parameters.Add("@IsActive", dimension.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", dimension.ModifierId, DbType.String);
                    parameters.Add("@BranchId", dimension.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDimension]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDimensionAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDimension]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<decimal> GetDimensionPriceByIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var price = await dbConnection.QueryFirstOrDefaultAsync<decimal>(
                        "[dbo].[SP_GetDimensionPriceById]", parameters, commandType: CommandType.StoredProcedure);

                    return price;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
