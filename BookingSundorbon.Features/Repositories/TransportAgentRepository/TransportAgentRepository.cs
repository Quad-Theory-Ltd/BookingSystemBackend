using BookingSundorbon.Views.DTOs.TransportAgentView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.TransportAgentRepository
{
    internal class TransportAgentRepository : ITransportAgentRepository
    {
        private readonly string _connectionString;

        public TransportAgentRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateTransportAgentAsync(TransportAgentView transportAgent)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@TransportAgentName", transportAgent.TransportAgentName, DbType.String);                    
                    parameters.Add("@TransportAgentAdddress", transportAgent.TransportAgentAdddress, DbType.String);
                    parameters.Add("@IsActive", transportAgent.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", transportAgent.CreatorId, DbType.String);
                    parameters.Add("@BranchId", transportAgent.BranchId, DbType.Int32);
                  

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoTransportAgent]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TransportAgentView> GetTransportAgentAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var transportAgent = await dbConnection.QueryFirstOrDefaultAsync<TransportAgentView>(
                        "[dbo].[SP_GetTransportAgentDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return transportAgent;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransportAgentView>> GetAllActiveTransportAgentsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var transportAgentes = await dbConnection.QueryAsync<TransportAgentView>(
                        "[dbo].[SP_GetAllActiveTransportAgents]", commandType: CommandType.StoredProcedure);

                    return transportAgentes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateTransportAgentAsync(TransportAgentView transportAgent)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", transportAgent.Id, DbType.Int32);
                    parameters.Add("@TransportAgentName", transportAgent.TransportAgentName, DbType.String);
                    parameters.Add("@TransportAgentAdddress", transportAgent.TransportAgentAdddress, DbType.String);
                    parameters.Add("@IsActive", transportAgent.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", transportAgent.ModifierId, DbType.String);
                    parameters.Add("@BranchId", transportAgent.BranchId, DbType.Int32);
                   


                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateTransportAgent]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task DeleteTransportAgentAsync(int id)
        //{
        //    try
        //    {
        //        using (IDbConnection dbConnection = new SqlConnection(_connectionString))
        //        {
        //            DynamicParameters parameters = new();
        //            parameters.Add("@Id", id, DbType.Int32);

        //            await dbConnection.ExecuteAsync(
        //                "[dbo].[SP_DeleteTransportAgent]", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
