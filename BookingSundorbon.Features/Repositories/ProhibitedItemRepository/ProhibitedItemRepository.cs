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
