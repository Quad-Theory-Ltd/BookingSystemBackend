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


namespace BookingSundorbon.Features.Repositories.ProhibitedItemRepository
{
    internal class ProhibitedItemRepository : IProhibitedItemRepository
    {
        private readonly string _connectionString;

        public ProhibitedItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateProhibitedItemAsync(ProhibitedItemView prohibitedItem)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", prohibitedItem.CompanyId, DbType.Int32);
                    parameters.Add("@ItemName", prohibitedItem.ItemName, DbType.String);
                    parameters.Add("@IsActive", prohibitedItem.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", prohibitedItem.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoProhibitedItem]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteProhibitedItemAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteProhibitedItem]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProhibitedItemView>> GetAllActiveProhibitedItemsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var items = await dbConnection.QueryAsync<ProhibitedItemView>("[dbo].[SP_GetAllActiveProhibitedItems]", commandType: CommandType.StoredProcedure);

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProhibitedItemView> GetProhibitedItemAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var prohibitedItem = await dbConnection.QueryFirstOrDefaultAsync<ProhibitedItemView>(
                        "[dbo].[SP_GetProhibitedItemDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return prohibitedItem;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateProhibitedItemAsync(ProhibitedItemView prohibiteditem)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", prohibiteditem.Id, DbType.Int32);
                    parameters.Add("@@CompanyId", prohibiteditem.CompanyId, DbType.Int32);
                    parameters.Add("@ItemName", prohibiteditem.ItemName, DbType.String);
                    parameters.Add("@IsActive", prohibiteditem.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", prohibiteditem.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateProhibitedItem]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
