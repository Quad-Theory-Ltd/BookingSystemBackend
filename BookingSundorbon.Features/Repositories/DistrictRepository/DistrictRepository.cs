using BookingSundorbon.Views.DTOs.BranchView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ActiveDistrictView;

namespace BookingSundorbon.Features.Repositories.DistrictRepository
{
    internal class DistrictRepository : IDistrictRepository
    {
        private readonly string _connectionString;

        public DistrictRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<ActiveDistrictView>> GetAllDistrictsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveDistrictView>("SP_GetAllActiveDistricts");

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
