using BookingSundorbon.Views.DTOs.ScreenView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.ScreenView;

namespace BookingSundorbon.Features.Repositories.ScreenRepository
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly string _connectionString;

        public ScreenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateScreenAsync(ScreenView screen)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", screen.Id, DbType.String);
                    parameters.Add("@UIName", screen.UIName, DbType.String);
                    parameters.Add("@IsActive", screen.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", screen.CreatorId, DbType.String);

                    await dbConnection.ExecuteScalarAsync<int>("[dbo].[SP_InsertIntoScreen]", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteScreenAsync(string id)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteScreen]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ScreenView>> GetAllActiveScreenesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var result = await dbConnection.QueryAsync<ScreenView>("SP_GetAllActiveScreens");

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<ScreenView> GetScreenAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.String);

                    var screen = await dbConnection.QueryFirstOrDefaultAsync<ScreenView>(
                        "[dbo].[SP_GetScreenDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return screen;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateScreenAsync(ScreenView screen)
        {

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", screen.Id, DbType.String);
                    parameters.Add("@UIName", screen.UIName, DbType.String);
                    parameters.Add("@IsActive", screen.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", screen.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateScreen]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
