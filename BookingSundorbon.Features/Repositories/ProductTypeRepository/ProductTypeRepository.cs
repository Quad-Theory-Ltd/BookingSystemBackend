using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ActiveProductTypeView;

namespace BookingSundorbon.Features.Repositories.ProductTypeRepository
{
    internal class ProductTypeRepository : IProductTypeRepository
    {
        private readonly string _connectionString;

        public ProductTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<ActiveProductTypeView>> GetAllActiveProductTypesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var productTypes = await dbConnection.QueryAsync<ActiveProductTypeView>(
                        "[dbo].[SP_GetAllActiveProductTypes]", commandType: CommandType.StoredProcedure);

                    return productTypes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
