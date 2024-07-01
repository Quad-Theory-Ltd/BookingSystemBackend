using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BookingSundorbon.Views.DTOs.CargoTypeView;

namespace BookingSundorbon.Features.Repositories.CargoTypeRepository
{
    internal class CargoTypeRepository : ICargoTypeRepository
    {

        private readonly string _connectionString;

        public CargoTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ActiveCargoTypeView>> GetAllActiveCargoTypesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveCargoTypeView>("SP_GetAllActiveCargoTypes");

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
