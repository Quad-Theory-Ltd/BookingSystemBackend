using BookingSundorbon.Views.DTOs.ParcelDetailsView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ParcelDetailsRepository
{
    internal class ParcelDetailsRepository : IParcelDetailsRepository
    {
        private readonly string _connectionString;

        public ParcelDetailsRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

  

        public async Task<ParcelDetailsView> GetParcelDetailsByParcelNoAsync(int parcelNo)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ParcelId", parcelNo, DbType.Int32);

                    var parcel = await dbConnection.QueryFirstOrDefaultAsync<ParcelDetailsView>(
                        "[dbo].[SP_GetParcelDetailsByParcelNo]", parameters, commandType: CommandType.StoredProcedure);

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
