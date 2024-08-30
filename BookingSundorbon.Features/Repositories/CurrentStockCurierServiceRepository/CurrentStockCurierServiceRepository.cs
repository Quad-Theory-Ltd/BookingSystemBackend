using BookingSundorbon.Views.DTOs.CurrentStockCurierServiceView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.CurrentStockCurierServiceRepository
{
    internal class CurrentStockCurierServiceRepository : ICurrentStockCurierServiceRepository
    {
        private readonly string _connectionString;

        public CurrentStockCurierServiceRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateCurrentStockCurierServiceAsync(CurrentStockCurierServiceView currentStockCurierService)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                   
                    parameters.Add("@DimentionId", currentStockCurierService.DimentionId, DbType.Int32);
                    parameters.Add("@TransactionDate", currentStockCurierService.TransactionDate, DbType.DateTime);
                    parameters.Add("@ReceiveQty", currentStockCurierService.ReceiveQty, DbType.Decimal);
                    parameters.Add("@IssueQty", currentStockCurierService.IssueQty, DbType.Decimal);
                    parameters.Add("@AdjustmentQty", currentStockCurierService.AdjustmentQty, DbType.Decimal);
                    parameters.Add("@BalanceQty", currentStockCurierService.BalanceQty, DbType.Decimal);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoCurrentStockCurierService]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CurrentStockCurierServiceView> GetCurrentStockCurierServiceAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var currentStockCurierService = await dbConnection.QueryFirstOrDefaultAsync<CurrentStockCurierServiceView>(
                        "[dbo].[SP_GetCurrentStockCurierServiceDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return currentStockCurierService;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CurrentStockCurierServiceView>> GetAllCurrentStockCurierServicesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var currentStockCurierServicees = await dbConnection.QueryAsync<CurrentStockCurierServiceView>(
                        "[dbo].[SP_GetAllCurrentStockCurierServices]", commandType: CommandType.StoredProcedure);

                    return currentStockCurierServicees;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCurrentStockCurierServiceAsync(CurrentStockCurierServiceView currentStockCurierService)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", currentStockCurierService.Id, DbType.Int32);
                    parameters.Add("@DimentionId", currentStockCurierService.DimentionId, DbType.Int32);
                    parameters.Add("@TransactionDate", currentStockCurierService.TransactionDate, DbType.DateTime);
                    parameters.Add("@ReceiveQty", currentStockCurierService.ReceiveQty, DbType.Decimal);
                    parameters.Add("@IssueQty", currentStockCurierService.IssueQty, DbType.Decimal);
                    parameters.Add("@AdjustmentQty", currentStockCurierService.AdjustmentQty, DbType.Decimal);
                    parameters.Add("@BalanceQty", currentStockCurierService.BalanceQty, DbType.Decimal);


                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateCurrentStockCurierService]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCurrentStockCurierServiceAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);


                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteCurrentStockCurierService]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
