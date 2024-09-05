using BookingSundorbon.Views.DTOs.ParcelView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelRepository
{
    internal class ParcelRepository : IParcelRepository
    {
        private readonly string _connectionString;

        public ParcelRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<int>> GetAllParcelNoAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var issueNo = await dbConnection.QueryAsync<int>(
                        "[dbo].[SP_GetAllParcelNo]", commandType: CommandType.StoredProcedure);

                    return issueNo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ScanningPersonView>> GetAllScanningPersonsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var person = await dbConnection.QueryAsync<ScanningPersonView>(
                        "[dbo].[SP_GetAllScanningPerson]", commandType: CommandType.StoredProcedure);

                    return person;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetLastParcelRecordSerialNoAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var lastParcelSerialNo = await dbConnection.ExecuteScalarAsync<string>(
                        "[dbo].[SP_LastParcelRecordSerialNo]", commandType: CommandType.StoredProcedure);

                    return lastParcelSerialNo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<AgentParcelView>> GetAllAgentParcelAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parcels = await dbConnection.QueryAsync<AgentParcelView>(
                        "[dbo].[Sp_GetAgentParcels]", commandType: CommandType.StoredProcedure);

                    return parcels;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<ParcelForBarcodeScanView> GetParcelInfoByIdAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var parcel = await dbConnection.QueryFirstOrDefaultAsync<ParcelForBarcodeScanView>(
                        "[dbo].[SP_GetParcelInfoById]", parameters, commandType: CommandType.StoredProcedure);

                    return parcel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AgentParcelView>> GetAgentParcelByAgentIdAsync(int agentId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@AgentId", agentId, DbType.Int32);

                    var parcels = await dbConnection.QueryAsync<AgentParcelView>(
                        "[dbo].[Sp_GetAgentParcelsByAgentId]", parameters, commandType: CommandType.StoredProcedure);

                    return parcels;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CheckParcelBarcode> CheckParcelBarcodeAsync(string barcode)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Barcode", barcode, DbType.String);

                    var isBarcodeMatched = await dbConnection.QueryFirstOrDefaultAsync<CheckParcelBarcode>(
                        "[dbo].[Sp_GetIsBarcodeMatch]", parameters, commandType: CommandType.StoredProcedure);

                    return isBarcodeMatched;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        //public async Task<int> CreateParcelAsync(ParcelView parcel)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@ParcelName", parcel.ParcelName, DbType.String);
        //            parameters.Add("@IPAddress", parcel.IPAddress, DbType.String);
        //            parameters.Add("@IsActive", parcel.IsActive, DbType.Boolean);
        //            parameters.Add("@CreatorId", parcel.CreatorId, DbType.String);
        //            parameters.Add("@BranchId", parcel.BranchId, DbType.Int32);

        //            var newId = await dbConnection.ExecuteScalarAsync<int>(
        //                "[dbo].[SP_InsertIntoParcel]", parameters, commandType: CommandType.StoredProcedure);

        //            return newId;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<ParcelView> GetParcelAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            var parcel = await dbConnection.QueryFirstOrDefaultAsync<ParcelView>(
        //                "[dbo].[SP_GetParcelDetailsById]", parameters, commandType: CommandType.StoredProcedure);

        //            return parcel;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<IEnumerable<ParcelView>> GetAllActiveParcelsAsync()
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            var parceles = await dbConnection.QueryAsync<ParcelView>(
        //                "[dbo].[SP_GetAllActiveParcels]", commandType: CommandType.StoredProcedure);

        //            return parceles;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task UpdateParcelAsync(ParcelView parcel)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", parcel.Id, DbType.Int32);
        //            parameters.Add("@ParcelName", parcel.ParcelName, DbType.String);
        //            parameters.Add("@IPAddress", parcel.IPAddress, DbType.String);
        //            parameters.Add("@IsActive", parcel.IsActive, DbType.Boolean);
        //            parameters.Add("@ModifierId", parcel.ModifierId, DbType.String);
        //            parameters.Add("@BranchId", parcel.BranchId, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_UpdateParcel]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async Task DeleteParcelAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteParcel]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
