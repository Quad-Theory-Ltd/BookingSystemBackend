using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.SenderView;

namespace BookingSundorbon.Features.Repositories.SenderRepository
{
    internal class SenderRepository : ISenderRepository
    {
        private readonly string _connectionString;

        public SenderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateSenderAsync(SenderView sender)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", sender.CompanyId, DbType.Int32);
                    parameters.Add("@DistrictId", sender.DistrictId, DbType.Int32);
                    parameters.Add("@CityId", sender.CityId, DbType.Int32);
                    parameters.Add("@Name", sender.Name, DbType.String);
                    parameters.Add("@Email", sender.Email, DbType.String);
                    parameters.Add("@Phone", sender.Phone, DbType.String);
                    parameters.Add("@NearestLandmark", sender.NearestLandmark, DbType.String);
                    parameters.Add("@Address", sender.Address, DbType.String);
                    parameters.Add("@IsActive", sender.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", sender.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoSender]", parameters, commandType: CommandType.StoredProcedure);

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
