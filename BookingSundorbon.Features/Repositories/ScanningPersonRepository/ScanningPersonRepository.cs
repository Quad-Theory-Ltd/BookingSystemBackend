using BookingSundorbon.Views.DTOs.ScanningPersonView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScanningPersonRepository
{
    internal class ScanningPersonRepository : IScanningPersonRepository
    {
        private readonly string _connectionString;

        public ScanningPersonRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateScanningPersonAsync(ScanningPersonView scanningPerson)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@UserId", scanningPerson.UserId, DbType.Int32);
                    parameters.Add("@Name", scanningPerson.Name, DbType.String);
                    parameters.Add("@ScanningPointName", scanningPerson.ScanningPointName, DbType.String);
                    parameters.Add("@IsActive", scanningPerson.IsActive, DbType.Boolean);
                    parameters.Add("@DeviceId", scanningPerson.DeviceId, DbType.Int32);
                    parameters.Add("@CreatorId", scanningPerson.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoScanningPerson]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ScanningPersonView> GetScanningPersonAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var scanningPerson = await dbConnection.QueryFirstOrDefaultAsync<ScanningPersonView>(
                        "[dbo].[SP_GetScanningPersonDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return scanningPerson;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ScanningPersonView>> GetAllActiveScanningPersonsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var scanningPersones = await dbConnection.QueryAsync<ScanningPersonView>(
                        "[dbo].[SP_GetAllActiveScanningPersons]", commandType: CommandType.StoredProcedure);

                    return scanningPersones;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateScanningPersonAsync(ScanningPersonView scanningPerson)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", scanningPerson.Id, DbType.Int32);
                    parameters.Add("@UserId", scanningPerson.UserId, DbType.Int32);
                    parameters.Add("@Name", scanningPerson.Name, DbType.String);
                    parameters.Add("@ScanningPointName", scanningPerson.ScanningPointName, DbType.String);
                    parameters.Add("@IsActive", scanningPerson.IsActive, DbType.Boolean);
                    parameters.Add("@DeviceId", scanningPerson.DeviceId, DbType.Int32);
                    parameters.Add("@ModifierId", scanningPerson.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateScanningPerson]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteScanningPersonAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteScanningPerson]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
