using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BookingSundorbon.Views.DTOs.ItemTypeView;


namespace BookingSundorbon.Features.Repositories.ItemTypeRepository
{
    internal class ItemTypeRepository :IItemTypeRepository
    {

        private readonly string _connectionString;

        public ItemTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ActiveItemTypeView>>GetAllActiveItemTypesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveItemTypeView>("SP_GetAllActiveItemTypes");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<int> CreateItemTypeAsync(ActiveItemTypeView itemType)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                   
                    parameters.Add("@CompanyId", itemType.CompanyId, DbType.Int32);
                    parameters.Add("@Cost", itemType.Cost, DbType.Decimal);
                    parameters.Add("@Name", itemType.Name, DbType.String);
                    parameters.Add("@IsActive", itemType.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", itemType.CreatorId, DbType.String);
                    parameters.Add("@BranchId", itemType.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoItemType]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActiveItemTypeView> GetItemTypeAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var itemType = await dbConnection.QueryFirstOrDefaultAsync<ActiveItemTypeView>(
                        "[dbo].[SP_GetItemTypeDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return itemType;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     


        public async Task UpdateItemTypeAsync(ActiveItemTypeView itemType)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", itemType.Id, DbType.Int32);
                    parameters.Add("@CompanyId", itemType.CompanyId, DbType.Int32);
                    parameters.Add("@Cost", itemType.Cost, DbType.Decimal);
                    parameters.Add("@Name", itemType.Name, DbType.String);
                    parameters.Add("@IsActive", itemType.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", itemType.ModifierId, DbType.String);
                    parameters.Add("@BranchId", itemType.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateItemType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteItemTypeAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteItemType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
