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



        public async Task<int> CreateCargoTypeAsync(ActiveCargoTypeView cargoType)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", cargoType.Id, DbType.Int32);
                    parameters.Add("@CompanyId", cargoType.CompanyId, DbType.Int32);
                    parameters.Add("@CargoTypeName", cargoType.CargoTypeName, DbType.String);
                    parameters.Add("@CargoCost", cargoType.CargoCost, DbType.Decimal);
                    parameters.Add("@IsActive", cargoType.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", cargoType.CreatorId, DbType.String);
                    parameters.Add("@BranchId", cargoType.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoCargoType]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ActiveCargoTypeView> GetCargoTypeAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var cargoType = await dbConnection.QueryFirstOrDefaultAsync<ActiveCargoTypeView>(
                        "[dbo].[SP_GetCargoTypeDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return cargoType;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActiveCargoTypeView>> GetAllActiveCargoTypesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ActiveCargoTypeView>("SP_GetAllActiveCargoTypes");

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task UpdateCargoTypeAsync(ActiveCargoTypeView cargoType)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", cargoType.Id, DbType.Int32);
                    parameters.Add("@CompanyId", cargoType.CompanyId, DbType.Int32);
                    parameters.Add("@CargoTypeName", cargoType.CargoTypeName, DbType.String);
                    parameters.Add("@CargoCost", cargoType.CargoCost, DbType.Decimal);
                    parameters.Add("@IsActive", cargoType.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", cargoType.ModifierId, DbType.String);
                    parameters.Add("@BranchId", cargoType.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateCargoType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCargoTypeAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteCargoType]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
