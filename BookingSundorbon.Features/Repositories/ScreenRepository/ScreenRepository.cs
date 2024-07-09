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

namespace BookingSundorbon.Features.Repositories.ScreenRepository
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly string _connectionString;

        public ScreenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateScreenAsync(CreateScreenView screen)
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

        public async Task CreateScreenFunctionAsync(CreateScreenFunctionView screenFunction)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", screenFunction.Id, DbType.String);
                    parameters.Add("@ScreenId", screenFunction.ScreenId, DbType.String);
                    parameters.Add("@FunctionId", screenFunction.FunctionId, DbType.String);
                    parameters.Add("@IsActive", screenFunction.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", screenFunction.CreatorId, DbType.String);

                    await dbConnection.ExecuteScalarAsync<int>("[dbo].[SP_InsertIntoScreenFunctions]", parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
