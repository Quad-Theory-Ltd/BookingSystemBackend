using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ParcelView;

namespace BookingSundorbon.Features.Repositories.ParcelRepository
{
    internal class ParcelRepository : IParcelRepository
    {
        private readonly string _connectionString;

        public ParcelRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<long> CreateParcelAsync(ParcelView parcel)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", parcel.CompanyId, DbType.Int32);
                    parameters.Add("@ProductTypeId", parcel.ProductTypeId, DbType.Int32);
                    parameters.Add("@NumberOfItem", parcel.NumberOfItem, DbType.Int32);
                    parameters.Add("@ParcelContentId", parcel.ParcelContentId, DbType.Int32);
                    parameters.Add("@PickUpDate", parcel.PickUpDate, DbType.DateTime);
                    parameters.Add("@HasPackaging", parcel.HasPackaging, DbType.Boolean);
                    parameters.Add("@HasInsurance", parcel.HasInsurance, DbType.Boolean);
                    parameters.Add("@IsFragileItem", parcel.IsFragileItem, DbType.Boolean);
                    parameters.Add("@IsInstantPickUp", parcel.IsInstantPickUp, DbType.Boolean);
                    parameters.Add("@Weight", parcel.Weight, DbType.Double);
                    parameters.Add("@Dimention", parcel.Dimention, DbType.Double);
                    parameters.Add("@AdditionalInfo", parcel.AdditionalInfo, DbType.String);
                    parameters.Add("@IsActive", parcel.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", parcel.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoParcel]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ParcelView> GetParcelAsync(long id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int64);

                    var parcel = await dbConnection.QueryFirstOrDefaultAsync<ParcelView>(
                        "[dbo].[SP_GetParcelDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return parcel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
