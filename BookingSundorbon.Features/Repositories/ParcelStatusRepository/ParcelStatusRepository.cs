using BookingSundorbon.Views.DTOs.ParcelStatusView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelStatusRepository
{
    internal class ParcelStatusRepository : IParcelStatusRepository
    {
        private readonly string _connectionString;

        public ParcelStatusRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateParcelStatusAsync(ParcelStatusView parcelStatus)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ParcelStatusName", parcelStatus.ParcelStatusName, DbType.String);                 
                    parameters.Add("@IsActive", parcelStatus.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", parcelStatus.CreatorId, DbType.String);
                    parameters.Add("@BranchId", parcelStatus.BranchId, DbType.Int32);
           

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoParcelStatus]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ParcelStatusView> GetParcelStatusAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);


                    var parcelStatus = await dbConnection.QueryFirstOrDefaultAsync<ParcelStatusView>(
                        "[dbo].[SP_GetParcelStatusDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return parcelStatus;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ParcelStatusView>> GetAllActiveParcelStatusAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parcelStatuses = await dbConnection.QueryAsync<ParcelStatusView>(
                        "[dbo].[SP_GetAllActiveParcelStatus]", commandType: CommandType.StoredProcedure);

                    return parcelStatuses;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateParcelStatusAsync(ParcelStatusView parcelStatus)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", parcelStatus.Id, DbType.Int32);
                    parameters.Add("@ParcelStatusName", parcelStatus.ParcelStatusName, DbType.String);
                    parameters.Add("@IsActive", parcelStatus.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", parcelStatus.ModifierId, DbType.String);
                    parameters.Add("@BranchId", parcelStatus.BranchId, DbType.Int32);


                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateParcelStatus]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        //public async Task DeleteParcelStatusAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteParcelStatus]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
