using BookingSundorbon.Views.DTOs.ActiveCityView;
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

        public async Task<int> CreateProhibitedItemAsync(CreateProhibitedItemView prohibitedItem)
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

        public async Task<IEnumerable<ProhibitedItemView>> GetAllActiveProhitedItemsAsync()
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

     
    }
}
