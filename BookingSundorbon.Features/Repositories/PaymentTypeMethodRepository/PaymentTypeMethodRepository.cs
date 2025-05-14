using BookingSundorbon.Views.DTOs.PaymentTypeMethodView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.PaymentTypeMethodRepository
{
    internal class PaymentTypeMethodRepository : IPaymentTypeMethodRepository
    {
        private readonly string _connectionString;

        public PaymentTypeMethodRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<PaymentTypeMethodView>> GetAllActivePaymentTypeMethodsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var paymentType = await dbConnection.QueryAsync<PaymentTypeMethodView>(
                        "[dbo].[SP_GetAllActivePaymentTypeMethods]", commandType: CommandType.StoredProcedure);

                    return paymentType;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
