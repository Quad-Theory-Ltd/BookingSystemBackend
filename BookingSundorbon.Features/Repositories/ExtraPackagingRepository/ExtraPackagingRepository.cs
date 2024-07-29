using BookingSundorbon.Views.DTOs.ExtraPackagingView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ExtraPackagingRepository
{
    internal class ExtraPackagingRepository : IExtraPackagingRepository
    {
        private readonly string _connectionString;

        public ExtraPackagingRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateExtraPackagingAsync(ExtraPackagingView extraPackaging)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@PackagingDescription", extraPackaging.PackagingDescription, DbType.String);
                    parameters.Add("@Cost", extraPackaging.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", extraPackaging.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", extraPackaging.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoExtraPackaging]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ExtraPackagingView> GetExtraPackagingAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var extraPackaging = await dbConnection.QueryFirstOrDefaultAsync<ExtraPackagingView>(
                        "[dbo].[SP_GetExtraPackagingDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return extraPackaging;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ExtraPackagingView>> GetAllActiveExtraPackagingsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var extraPackaginges = await dbConnection.QueryAsync<ExtraPackagingView>(
                        "[dbo].[SP_GetAllActiveExtraPackaging]", commandType: CommandType.StoredProcedure);

                    return extraPackaginges;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateExtraPackagingAsync(ExtraPackagingView extraPackaging)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", extraPackaging.Id, DbType.Int32);
                    parameters.Add("@PackagingDescription", extraPackaging.PackagingDescription, DbType.String);
                    parameters.Add("@Cost", extraPackaging.Cost, DbType.Decimal);
                    parameters.Add("@IsActive", extraPackaging.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", extraPackaging.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateExtraPackaging]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteExtraPackagingAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteExtraPackaging]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
