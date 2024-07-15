using BookingSundorbon.Views.DTOs.BarcodeStatusView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeStatusRepository
{
    internal class BarcodeStatusRepository : IBarcodeStatusRepository
    {
        private readonly string _connectionString;

        public BarcodeStatusRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateBarcodeStatusAsync(BarcodeStatusView barcodeStatus)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@BarcodeNumber", barcodeStatus.BarcodeNumber, DbType.String);
                    parameters.Add("@IsActive", barcodeStatus.IsActive, DbType.Boolean);
                    parameters.Add("@UserId", barcodeStatus.UserId, DbType.Int32);
                    parameters.Add("@CreatorId", barcodeStatus.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoBarcodeStatus]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BarcodeStatusView> GetBarcodeStatusAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var barcodeStatus = await dbConnection.QueryFirstOrDefaultAsync<BarcodeStatusView>(
                        "[dbo].[SP_GetBarcodeStatusDetailById]", parameters, commandType: CommandType.StoredProcedure);

                    return barcodeStatus;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BarcodeStatusView>> GetAllActiveBarcodeStatusAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var barcodeStatuses = await dbConnection.QueryAsync<BarcodeStatusView>(
                        "[dbo].[SP_GetAllActiveBarcodeStatus]", commandType: CommandType.StoredProcedure);

                    return barcodeStatuses;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateBarcodeStatusAsync(BarcodeStatusView barcodeStatus)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", barcodeStatus.Id, DbType.Int32);
                    parameters.Add("@BarcodeNumber", barcodeStatus.BarcodeNumber, DbType.String);
                    parameters.Add("@IsActive", barcodeStatus.IsActive, DbType.Boolean);
                    parameters.Add("@UserId", barcodeStatus.UserId, DbType.Int32);
                    parameters.Add("@ModifierId", barcodeStatus.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateBarcodeStatus]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBarcodeStatusAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteBarcodeStatus]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
