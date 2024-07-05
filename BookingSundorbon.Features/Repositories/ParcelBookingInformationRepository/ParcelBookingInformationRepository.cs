using BookingSundorbon.Views.DTOs.ParcelBookingInformationView;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookingSundorbon.Features.Repositories.ParcelBookingInformationRepository
{
    internal class ParcelBookingInformationRepository : IParcelBookingInformationRepository
    {
        private readonly string _connectionString;

        public ParcelBookingInformationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ParcelInfoByUserIdView>> GetParcelInfoByUserIdAsync(string userId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", userId, DbType.String);

                    var result = await dbConnection.QueryAsync<ParcelInfoByUserIdView>(
                        "[dbo].[SP_GetParcelInfoByUserId]", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
