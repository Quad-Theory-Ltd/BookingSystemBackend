using BookingSundorbon.Views.DTOs.DepartmentView;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.DepartmentRepository
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDepartmentAsync(DepartmentView department)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@CompanyId", department.CompanyId, DbType.Int32);
                    parameters.Add("@BranchId", department.BranchId, DbType.Int32);
                    parameters.Add("@DepartmentName", department.DepartmentName, DbType.String);
                    parameters.Add("@CreatorId", department.CreatorId, DbType.String);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoDepartment]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DepartmentView> GetDepartmentAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var department = await dbConnection.QueryFirstOrDefaultAsync<DepartmentView>(
                        "[dbo].[SP_GetDepartmentDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return department;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentView>> GetAllDepartmentsAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var departmentes = await dbConnection.QueryAsync<DepartmentView>(
                        "[dbo].[SP_GetAllDepartments]", commandType: CommandType.StoredProcedure);

                    return departmentes;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDepartmentAsync(DepartmentView department)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", department.Id, DbType.Int32);
                    parameters.Add("@CompanyId", department.CompanyId, DbType.Int32);
                    parameters.Add("@BranchId", department.BranchId, DbType.Int32);
                    parameters.Add("@DepartmentName", department.DepartmentName, DbType.String);
                    parameters.Add("@ModifierId", department.ModifierId, DbType.String);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateDepartment]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteDepartment]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
