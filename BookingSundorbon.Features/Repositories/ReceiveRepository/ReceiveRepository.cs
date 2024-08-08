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

 

    }
}
