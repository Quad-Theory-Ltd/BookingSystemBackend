using BookingSundorbon.Views.DTOs.ScanningPointView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScanningPointRepository
{
    internal class ScanningPointRepository : IScanningPointRepository
    {
        private readonly string _connectionString;

        public ScanningPointRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateScanningPointAsync(ScanningPointView scanningPoint)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", scanningPoint.UserId, DbType.Int32);
                    parameters.Add("@Name", scanningPoint.Name, DbType.String);
                    parameters.Add("@ScanningPointName", scanningPoint.ScanningPointName, DbType.String);
                    parameters.Add("@ParcelStatusId", scanningPoint.ParcelStatusId, DbType.Int32);
                    parameters.Add("@DeviceId", scanningPoint.DeviceId, DbType.Int32);
                    parameters.Add("@IsAgent", scanningPoint.IsAgent, DbType.Boolean);
                    parameters.Add("@IsActive", scanningPoint.IsActive, DbType.Boolean);                   
                    parameters.Add("@CreatorId", scanningPoint.CreatorId, DbType.String);
                    parameters.Add("@BranchId", scanningPoint.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoScanningPoint]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ScanningPointView> GetScanningPointAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var scanningPoint = await dbConnection.QueryFirstOrDefaultAsync<ScanningPointView>(
                        "[dbo].[SP_GetScanningPointDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return scanningPoint;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ScanningPointView>> GetAllActiveScanningPointsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var scanningPointes = await dbConnection.QueryAsync<ScanningPointView>(
                        "[dbo].[SP_GetAllActiveScanningPoints]", commandType: CommandType.StoredProcedure);

                    return scanningPointes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateScanningPointAsync(ScanningPointView scanningPoint)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", scanningPoint.Id, DbType.Int32);
                    parameters.Add("@UserId", scanningPoint.UserId, DbType.Int32);
                    parameters.Add("@Name", scanningPoint.Name, DbType.String);
                    parameters.Add("@ScanningPointName", scanningPoint.ScanningPointName, DbType.String);
                    parameters.Add("@ParcelStatusId", scanningPoint.ParcelStatusId, DbType.Int32);
                    parameters.Add("@DeviceId", scanningPoint.DeviceId, DbType.Int32);
                    parameters.Add("@IsAgent", scanningPoint.IsAgent, DbType.Boolean);
                    parameters.Add("@IsActive", scanningPoint.IsActive, DbType.Boolean);
                    parameters.Add("@@ModifierId", scanningPoint.CreatorId, DbType.String);
                    parameters.Add("@BranchId", scanningPoint.BranchId, DbType.Int32);


                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateScanningPoint]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteScanningPointAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteScanningPoint]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
