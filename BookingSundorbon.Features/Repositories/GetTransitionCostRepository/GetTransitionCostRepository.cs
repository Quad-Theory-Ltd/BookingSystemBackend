using BookingSundorbon.Views.DTOs.ProhibitedItemView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using BookingSundorbon.Views.DTOs.GetTransitionCostView;

namespace BookingSundorbon.Features.Repositories.GetTransitionCostRepository
{
    public class GetTransitionCostRepository : IGetTransitionCostRepository
    {
        private readonly string _connectionString;

        public GetTransitionCostRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<decimal> GetTransitionCostWithoutVat(GetTransitionCostView transitionCostView)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ParcelLength", transitionCostView.ParcelLength, DbType.Decimal);
                    parameters.Add("@ParcelWidth", transitionCostView.ParcelWidth, DbType.Decimal);
                    parameters.Add("@ParcelHeight", transitionCostView.ParcelHeight, DbType.Decimal);
                    parameters.Add("@ParcelWeight", transitionCostView.ParcelWeight, DbType.Decimal);
                    parameters.Add("@CargoType", transitionCostView.CargoType, DbType.String);
                    parameters.Add("@IsExtraPackaging", transitionCostView.IsExtraPackaging, DbType.Boolean);
                    parameters.Add("@IsFragileItem", transitionCostView.IsFragileItem, DbType.Boolean);
                   // parameters.Add("@RoutingTypeId", transitionCostView.RoutingTypeId, DbType.Int32);


                    decimal cost = await dbConnection.ExecuteScalarAsync<decimal>(
                        "[dbo].[SP_GetCalculateTotalPricing]", parameters, commandType: CommandType.StoredProcedure);

                   
                    return cost;
                }
            }
            catch (Exception ex) {
                throw;
            }
            
        }
            
}
    }
