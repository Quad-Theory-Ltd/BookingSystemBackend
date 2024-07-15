using BookingSundorbon.Views.DTOs.BarcodeStatusDetailView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeStatusDetailRepository
{
    internal class BarcodeStatusDetailRepository : IBarcodeStatusDetailRepository
    {
        private readonly string _connectionString;

        public BarcodeStatusDetailRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateBarcodeStatusDetailAsync(BarcodeStatusDetailView barcodeStatusDetail)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@BarcodeStatusId", barcodeStatusDetail.BarcodeStatusId, DbType.Int32);
                    parameters.Add("@ScannerPersonId", barcodeStatusDetail.ScannerPersonId, DbType.Int32);
                    parameters.Add("@IsActive", barcodeStatusDetail.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", barcodeStatusDetail.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoBarcodeStatusDetails]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BarcodeStatusDetailView> GetBarcodeStatusDetailAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var barcodeStatusDetail = await dbConnection.QueryFirstOrDefaultAsync<BarcodeStatusDetailView>(
                        "[dbo].[SP_GetBarcodeStatusDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return barcodeStatusDetail;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BarcodeStatusDetailView>> GetAllActiveBarcodeStatusDetailAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var barcodeStatusDetailes = await dbConnection.QueryAsync<BarcodeStatusDetailView>(
                        "[dbo].[SP_GetAllActiveBarcodeStatusDetails]", commandType: CommandType.StoredProcedure);

                    return barcodeStatusDetailes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateBarcodeStatusDetailAsync(BarcodeStatusDetailView barcodeStatusDetail)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", barcodeStatusDetail.Id, DbType.Int32);
                    parameters.Add("@BarcodeStatusId", barcodeStatusDetail.BarcodeStatusId, DbType.Int32);
                    parameters.Add("@ScannerPersonId", barcodeStatusDetail.ScannerPersonId, DbType.Int32);
                    parameters.Add("@IsActive", barcodeStatusDetail.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", barcodeStatusDetail.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateBarcodeStatusDetails]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBarcodeStatusDetailAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteBarcodeStatusDetails]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
