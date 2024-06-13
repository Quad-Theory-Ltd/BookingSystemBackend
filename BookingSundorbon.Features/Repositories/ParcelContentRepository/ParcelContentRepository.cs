using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ActiveParcelContentView;

namespace BookingSundorbon.Features.Repositories.ParcelContentRepository
{
    internal class ParcelContentRepository : IParcelContentRepository
    {
        private readonly string _connectionString;

        public ParcelContentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<ActiveParcelContentView>> GetAllActiveParcelContentsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parcelContents = await dbConnection.QueryAsync<ActiveParcelContentView>(
                        "[dbo].[SP_GetAllActiveParcelContents]", commandType: CommandType.StoredProcedure);

                    return parcelContents;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
