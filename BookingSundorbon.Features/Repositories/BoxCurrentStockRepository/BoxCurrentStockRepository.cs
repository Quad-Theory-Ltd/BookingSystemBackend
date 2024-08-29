using BookingSundorbon.Views.DTOs.BoxCurrentStockView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.BoxCurrentStockRepository
{
    internal class BoxCurrentStockRepository : IBoxCurrentStockRepository
    {
        private readonly string _connectionString;

        public BoxCurrentStockRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateBoxCurrentStockAsync(BoxCurrentStockView boxCurrentStock)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    
                    parameters.Add("@DimensionId", boxCurrentStock.DimensionId, DbType.Int32);
                    parameters.Add("@AgentId", boxCurrentStock.AgentId, DbType.Int32);
                    parameters.Add("@BranchId", boxCurrentStock.BranchId, DbType.Int32);
                    parameters.Add("@SubbranchId", boxCurrentStock.SubbranchId, DbType.Int32);
                    parameters.Add("@DamageQty", boxCurrentStock.DamageQty, DbType.Int32);
                    parameters.Add("@CurrentStockQty", boxCurrentStock.CurrentStockQty, DbType.Int32);
                    parameters.Add("@CreatorId", boxCurrentStock.CreatorId, DbType.String);
                   

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoBoxCurrentStock]", parameters, commandType: CommandType.StoredProcedure);


                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BoxCurrentStockView> GetBoxCurrentStockAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var boxCurrentStock = await dbConnection.QueryFirstOrDefaultAsync<BoxCurrentStockView>(
                        "[dbo].[SP_GetBoxCurrentStockDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return boxCurrentStock;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BoxCurrentStockView>> GetAllBoxCurrentStocksAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var boxCurrentStockes = await dbConnection.QueryAsync<BoxCurrentStockView>(
                        "[dbo].[SP_GetAllBoxCurrentStocks]", commandType: CommandType.StoredProcedure);

                    return boxCurrentStockes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateBoxCurrentStockAsync(BoxCurrentStockView boxCurrentStock)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", boxCurrentStock.Id, DbType.Int32);
                    parameters.Add("@DimensionId", boxCurrentStock.DimensionId, DbType.Int32);
                    parameters.Add("@AgentId", boxCurrentStock.AgentId, DbType.Int32);
                    parameters.Add("@BranchId", boxCurrentStock.BranchId, DbType.Int32);
                    parameters.Add("@SubbranchId", boxCurrentStock.SubbranchId, DbType.Int32);
                    parameters.Add("@DamageQty", boxCurrentStock.DamageQty, DbType.Int32);
                    parameters.Add("@CurrentStockQty", boxCurrentStock.CurrentStockQty, DbType.Int32);
                    parameters.Add("@ModifierId", boxCurrentStock.ModifierId, DbType.String);
              

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateBoxCurrentStock]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBoxCurrentStockAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteBoxCurrentStock]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
