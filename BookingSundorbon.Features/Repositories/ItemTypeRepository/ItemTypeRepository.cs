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
    }
}
