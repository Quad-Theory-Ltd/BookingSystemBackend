using BookingSundorbon.Views.DTOs.PaymentView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.PaymentRepository
{
    internal class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreatePaymentAsync(PaymentView payment)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();

                    parameters.Add("@PaymentMethodId", payment.PaymentMethodId, DbType.Int32);
                    parameters.Add("@ParcelOderNo", payment.ParcelOderNo, DbType.String);
                    parameters.Add("@OrderAmount", payment.OrderAmount, DbType.Decimal);
                    parameters.Add("@Description", payment.Description, DbType.String);                    
                    parameters.Add("@CreatorId", payment.CreatorId, DbType.String);
                    parameters.Add("@PaymentInvoiceNo", payment.PaymentInvoiceNo, DbType.String);
                    parameters.Add("@PaymentDate", payment.PaymentDate, DbType.DateTime);
       

        var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoPayment]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaymentView> GetPaymentAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var payment = await dbConnection.QueryFirstOrDefaultAsync<PaymentView>(
                        "[dbo].[SP_GetPaymentDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return payment;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PaymentView>> GetAllPaymentsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var paymentes = await dbConnection.QueryAsync<PaymentView>(
                        "[dbo].[SP_GetAllPayments]", commandType: CommandType.StoredProcedure);

                    return paymentes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdatePaymentAsync(PaymentView payment)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", payment.Id, DbType.Int32);
                    parameters.Add("@PaymentMethodId", payment.PaymentMethodId, DbType.Int32);
                    parameters.Add("@ParcelOderNo", payment.ParcelOderNo, DbType.String);
                    parameters.Add("@OrderAmount", payment.OrderAmount, DbType.Decimal);
                    parameters.Add("@Description", payment.Description, DbType.String);
                    parameters.Add("@ModifierId", payment.ModifierId, DbType.String);
                    parameters.Add("@PaymentInvoiceNo", payment.PaymentInvoiceNo, DbType.String);
                    parameters.Add("@PaymentDate", payment.PaymentDate, DbType.DateTime);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdatePayment]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeletePaymentAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeletePayment]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PaymentView> GetPaymentAsyncByParcelNoAsync(string parcelId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@ParcelId", parcelId, DbType.Int32);

                    var payment = await dbConnection.QueryFirstOrDefaultAsync<PaymentView>(
                        "[dbo].[SP_GetPaymentByparcelId]", parameters, commandType: CommandType.StoredProcedure);

                    return payment;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
