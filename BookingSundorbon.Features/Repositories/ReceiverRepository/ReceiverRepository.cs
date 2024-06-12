using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ReceiverView;

namespace BookingSundorbon.Features.Repositories.ReceiverRepository
{
    internal class ReceiverRepository : IReceiverRepository
    {
        private readonly string _connectionString;

        public ReceiverRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<long> CreateReceiverAsync(ReceiverView receiver)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", receiver.CompanyId, DbType.Int32);
                    parameters.Add("@DistrictId", receiver.DistrictId, DbType.Int32);
                    parameters.Add("@CityId", receiver.CityId, DbType.Int32);
                    parameters.Add("@Name", receiver.Name, DbType.String);
                    parameters.Add("@Email", receiver.Email, DbType.String);
                    parameters.Add("@Phone", receiver.Phone, DbType.String);
                    parameters.Add("@NearestLandmark", receiver.NearestLandmark, DbType.String);
                    parameters.Add("@Address", receiver.Address, DbType.String);
                    parameters.Add("@IsActive", receiver.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", receiver.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoReceiver]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ReceiverView> GetReceiverAsync(long id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int64);

                    var receiver = await dbConnection.QueryFirstOrDefaultAsync<ReceiverView>(
                        "[dbo].[SP_GetReceiverDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return receiver;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
