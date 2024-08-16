using BookingSundorbon.Views.DTOs.TransportAgentCostView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.TransportAgentCostRepository
{
    internal class TransportAgentCostRepository : ITransportAgentCostRepository
    {
        private readonly string _connectionString;

        public TransportAgentCostRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateTransportAgentCostAsync(TransportAgentCostView transportAgentCost)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();

                    parameters.Add("@ParcelNo", transportAgentCost.parcelNo, DbType.Int32);
                    parameters.Add("@PickUpPoint", transportAgentCost.PickUpPoint, DbType.String);
                    parameters.Add("@DeliveryPoint", transportAgentCost.DeliveryPoint, DbType.String);
                    parameters.Add("@TransportAgentId", transportAgentCost.TransportAgentId, DbType.Int32);
                    parameters.Add("@Cost", transportAgentCost.Cost, DbType.Decimal);
 
                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoTransportAgentCost]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TransportAgentCostView> GetTransportAgentCostAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var transportAgentCost = await dbConnection.QueryFirstOrDefaultAsync<TransportAgentCostView>(
                        "[dbo].[SP_GetTransportAgentCostDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return transportAgentCost;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransportAgentCostView>> GetAllTransportAgentCostsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var transportAgentCostes = await dbConnection.QueryAsync<TransportAgentCostView>(
                        "[dbo].[SP_GetAllTransportAgentCosts]", commandType: CommandType.StoredProcedure);

                    return transportAgentCostes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task UpdateTransportAgentCostAsync(TransportAgentCostView transportAgentCost)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            // parameters ......... 

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_UpdateTransportAgentCost]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        //public async Task DeleteTransportAgentCostAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteTransportAgentCost]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
