using BookingSundorbon.Views.DTOs.BarcodeScanView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BarcodeScanRepository
{
    internal class BarcodeScanRepository : IBarcodeScanRepository
    {
        private readonly string _connectionString;

        public BarcodeScanRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateBarcodeScanAsync(BarcodeScanView barcodeScan)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ParcelNo",barcodeScan.ParcelNo, DbType.Int32);
                    parameters.Add("@BarcodeNo", barcodeScan.BarcodeNo, DbType.String);                   
                    parameters.Add("@IsActive", 1, DbType.Boolean);
                    parameters.Add("@CreatorId", barcodeScan.CreatorId, DbType.String);
                    parameters.Add("@ScanningPointId", barcodeScan.ScanningPointId, DbType.Int32);
                    parameters.Add("@ScanningPersonId", barcodeScan.ScanningPersonId, DbType.Int32);
                    parameters.Add("@ParcelStatusId",barcodeScan.ParcelStatusId, DbType.Int32);


                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoBarcodeScan]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BarcodeScanView> GetBarcodeScanAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var barcodeScan = await dbConnection.QueryFirstOrDefaultAsync<BarcodeScanView>(
                        "[dbo].[SP_GetBarcodeScanDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return barcodeScan;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BarcodeScanView>> GetAllActiveBarcodeScansAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var barcodeScanes = await dbConnection.QueryAsync<BarcodeScanView>(
                        "[dbo].[SP_GetAllActiveBarcodeScans]", commandType: CommandType.StoredProcedure);

                    return barcodeScanes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task UpdateBarcodeScanAsync(BarcodeScanView barcodeScan)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", barcodeScan.Id, DbType.Int32);
        //            parameters.Add("@ParcelNo");
        //            parameters.Add("@BarcodeNo");
        //            parameters.Add("@IsActive", barcodeScan.IsActive, DbType.Boolean);
        //            parameters.Add("@CreatorId", barcodeScan.CreatorId, DbType.String);
        //            parameters.Add("@ScanningPointId");
        //            parameters.Add("@ScanningPersonId");
        //            parameters.Add("@ParcelStatusId");



        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_UpdateBarcodeScan]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task DeleteBarcodeScanAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteBarcodeScan]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
