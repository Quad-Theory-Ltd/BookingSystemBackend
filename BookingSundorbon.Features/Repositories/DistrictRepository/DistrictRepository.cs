using BookingSundorbon.Views.DTOs.BranchView;
using BookingSundorbon.Views.DTOs.DistrictView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DistrictRepository
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly string _connectionString;
        public DistrictRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task<List<ActiveDistrictView>> GetAllDistrictsAsync()
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

        public async Task<ActiveDistrictView> GetDistrictByIdAsync(int id, bool isActive)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@id", id, DbType.Int32);
                    parameters.Add("@isActive", isActive, DbType.Boolean);

                    var result = await dbConnection.QueryFirstOrDefaultAsync<ActiveDistrictView>("SP_GetDistrictById", parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
