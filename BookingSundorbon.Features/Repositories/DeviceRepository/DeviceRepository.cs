using BookingSundorbon.Views.DTOs.DeviceView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DeviceRepository
{
    internal class DeviceRepository : IDeviceRepository
    {
        private readonly string _connectionString;

        public DeviceRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDeviceAsync(DeviceView device)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@DeviceName", device.DeviceName, DbType.String);
                    parameters.Add("@IPAddress", device.IPAddress, DbType.String);
                    parameters.Add("@IsActive", device.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", device.CreatorId, DbType.String);
                    parameters.Add("@BranchId", device.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDevice]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DeviceView> GetDeviceAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var device = await dbConnection.QueryFirstOrDefaultAsync<DeviceView>(
                        "[dbo].[SP_GetDeviceDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return device;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DeviceView>> GetAllActiveDevicesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var devicees = await dbConnection.QueryAsync<DeviceView>(
                        "[dbo].[SP_GetAllActiveDevices]", commandType: CommandType.StoredProcedure);

                    return devicees;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDeviceAsync(DeviceView device)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", device.Id, DbType.Int32);
                    parameters.Add("@DeviceName", device.DeviceName, DbType.String);
                    parameters.Add("@IPAddress", device.IPAddress, DbType.String);
                    parameters.Add("@IsActive", device.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", device.ModifierId, DbType.String);
                    parameters.Add("@BranchId", device.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDevice]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDeviceAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDevice]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
