using BookingSundorbon.Views.DTOs.ItemCategoryView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ItemCategoryRepository
{
    internal class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly string _connectionString;

        public ItemCategoryRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateItemCategoryAsync(ItemCategoryView itemCategory)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", itemCategory.CompanyId, DbType.Int32);
                    parameters.Add("@Name", itemCategory.Name, DbType.String);
                    parameters.Add("@IsActive", itemCategory.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", itemCategory.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoItemCategory]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ItemCategoryView> GetItemCategoryAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var itemCategory = await dbConnection.QueryFirstOrDefaultAsync<ItemCategoryView>(
                        "[dbo].[SP_GetItemCategoryDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return itemCategory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ItemCategoryView>> GetAllActiveItemCategoriesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var itemCategoryes = await dbConnection.QueryAsync<ItemCategoryView>(
                        "[dbo].[SP_GetAllActiveItemCategories]", commandType: CommandType.StoredProcedure);

                    return itemCategoryes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateItemCategoryAsync(ItemCategoryView itemCategory)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", itemCategory.Id, DbType.Int32);
                    parameters.Add("@CompanyId", itemCategory.CompanyId, DbType.Int32);
                    parameters.Add("@Name", itemCategory.Name, DbType.String);
                    parameters.Add("@IsActive", itemCategory.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", itemCategory.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateItemCategory]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteItemCategoryAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteItemCategory]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
