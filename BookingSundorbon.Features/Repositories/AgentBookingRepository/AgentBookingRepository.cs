using BookingSundorbon.Views.DTOs.AgentBookingView;
using BookingSundorbon.Views.DTOs.BranchView;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BookingSundorbon.Features.Repositories.AgentBookingRepository
{
    internal class AgentBookingRepository : IAgentBookingRepository
    {
        private readonly string _connectionString;

        public AgentBookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<AgentBookingCountByDimensionView>> GetAgentBookingCountsByDimensionAsync(string id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@AgentId", id, DbType.String);

                    var result = await dbConnection.QueryAsync<AgentBookingCountByDimensionView>(
                        "[dbo].[SP_GetAgentBookingCountsByDimension]", parameters, commandType: CommandType.StoredProcedure);


                    return result;
                }

            }
            catch (Exception ex) {
                throw;
            }
        }
           
}
    }

