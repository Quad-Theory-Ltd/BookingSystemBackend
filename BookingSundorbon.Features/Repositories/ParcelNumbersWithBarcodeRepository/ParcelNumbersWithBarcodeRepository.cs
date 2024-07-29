using BookingSundorbon.Features.Repositories.ParcelContentRepository;
using BookingSundorbon.Views.DTOs.ActiveParcelContentView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ParcelNumbersWithBarcodeView;
using Dapper;

namespace BookingSundorbon.Features.Repositories.ParcelNumbersWithBarcodeRepository
{
    public class ParcelNumbersWithBarcodeRepository : IParcelNumbersWithBarcodeRepository
    {
        private readonly string _connectionString;

        public ParcelNumbersWithBarcodeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<IEnumerable<ParcelNumbersWithBarcodeView>> GetAllParcelNumbersWithBarcodes()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parcelContents = await dbConnection.QueryAsync<ParcelNumbersWithBarcodeView>(
                        "[dbo].[SP_GetAllParcelNumbersWithBarcodes]", commandType: CommandType.StoredProcedure);

                    return parcelContents;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<ParcelNumbersWithBarcodeView>> GetAgentParcelNumberrsWithBarcodes()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var parcelContents = await dbConnection.QueryAsync<ParcelNumbersWithBarcodeView>(
                        "[dbo].[SP_GetAgentParcelNumbersWithBarcodes]", commandType: CommandType.StoredProcedure);

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
