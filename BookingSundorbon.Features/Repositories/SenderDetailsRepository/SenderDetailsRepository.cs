using BookingSundorbon.Views.DTOs.SenderDetailsView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.SenderDetails;

namespace BookingSundorbon.Features.Repositories.SenderDetailsRepository
{
    internal class SenderDetailsRepository : ISenderDetailsRepository
    {
        private readonly string _connectionString;

        public SenderDetailsRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<PickUpAndDeliveryInfoView> GetPickupAndDeliveryPointAsync(int parcelOrderId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@parcelOrderId", parcelOrderId, DbType.Int32);

                    var point = await dbConnection.QueryFirstOrDefaultAsync<PickUpAndDeliveryInfoView>(
                        "[dbo].[SP_GetPickupAndDeliveryPointByParcelOrderId]", parameters, commandType: CommandType.StoredProcedure);

                    return point;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      

    
    }
}
