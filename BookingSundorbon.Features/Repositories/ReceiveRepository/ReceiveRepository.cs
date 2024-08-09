using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ReceiveView;
using BookingSundorbon.Views.DTOs.IssueView;


namespace BookingSundorbon.Features.Repositories.ReceiveRepository
{
    internal class ReceiveRepository:IReceiveRepository
    {
        private readonly string _connectionString;

        public ReceiveRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<int> CreateReceiveAsync(ReceiveView receive)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ReceiveDate", receive.ReceiveDate, DbType.DateTime);
                    parameters.Add ("@IssueNo" , receive.IssueNo, DbType.Int32);
                    parameters.Add("@CreatorId", receive.CreatorId, DbType.String);

                    
                    parameters.Add("@ReceivedBy", receive.ReceivedBy, DbType.Int32);
                    parameters.Add("@ReceivedPrice", receive.ReceivedPrice, DbType.Decimal);
                    parameters.Add("@Remarks", receive.Remarks, DbType.String);
                    parameters.Add("@ReceivedQty", receive.ReceivedQty, DbType.Int32);
                    parameters.Add("@DimensionId", receive.DimensionId, DbType.Int32);



                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoReceive]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ReceiveView>> GetAllReceiveAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var receive = await dbConnection.QueryAsync<ReceiveView>(
                        "[dbo].[SP_GetAllReceive]", commandType: CommandType.StoredProcedure);

                    return receive;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ReceiveView> GetReceiveAsync(int receiveNo)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ReceiveNo", receiveNo, DbType.Int32);

                    var receive = await dbConnection.QueryFirstOrDefaultAsync<ReceiveView>(
                        "[dbo].[SP_GetReceiveDetailsByReceiveNo]", parameters, commandType: CommandType.StoredProcedure);

                    return receive;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetNextReceiveNoAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var requisitionNumber = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_GetLastReceiveNo]", commandType: CommandType.StoredProcedure);

                    return requisitionNumber + 1;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
