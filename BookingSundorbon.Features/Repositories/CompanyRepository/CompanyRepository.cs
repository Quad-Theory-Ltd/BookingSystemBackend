using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSundorbon.Views.DTOs.CompanyView;

namespace BookingSundorbon.Features.Repositories.CompanyRepository
{
    internal class CompanyRepository:ICompanyRepository
    {
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateCompanyAsync(CompanyView company)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Name", company.Name, DbType.String);
                    parameters.Add("@CompanyWebsite", company.CompanyWebsite, DbType.String);
                    parameters.Add("@BIN", company.BIN, DbType.String);
                    parameters.Add("@TIN", company.TIN, DbType.String);
                    parameters.Add("@VATRegNo", company.VATRegNo, DbType.String);
                    parameters.Add("@IsActive", company.IsActive, DbType.Boolean);
                    parameters.Add("@CreatorId", company.CreatorId, DbType.String);
                    parameters.Add("@BranchId", company.BranchId, DbType.Int32);

                    var newId = await dbConnection.ExecuteScalarAsync<int>(
                        "[dbo].[SP_InsertIntoCompany]", parameters, commandType: CommandType.StoredProcedure);

                    return newId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CompanyView> GetCompanyAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    var company = await dbConnection.QueryFirstOrDefaultAsync<CompanyView>(
                        "[dbo].[SP_GetCompanayDetailsById]", parameters, commandType: CommandType.StoredProcedure);

                    return company;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompanyView>> GetAllCompaniesAsync()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    var companies = await dbConnection.QueryAsync<CompanyView>(
                        "[dbo].[SP_GetAllCompanies]", commandType: CommandType.StoredProcedure);

                    return companies;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCompanyAsync(CompanyView company)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", company.Id, DbType.Int32);
                    parameters.Add("@Name", company.Name, DbType.String);
                    parameters.Add("@CompanyWebsite", company.CompanyWebsite, DbType.String);
                    parameters.Add("@BIN", company.BIN, DbType.String);
                    parameters.Add("@TIN", company.TIN, DbType.String);
                    parameters.Add("@VATRegNo", company.VATRegNo, DbType.String);
                    parameters.Add("@IsActive", company.IsActive, DbType.Boolean);
                    parameters.Add("@ModifierId", company.ModifierId, DbType.String);
                    parameters.Add("@BranchId", company.BranchId, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_UpdateCompany]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    DynamicParameters parameters = new();
                    parameters.Add("@Id", id, DbType.Int32);

                    await dbConnection.ExecuteAsync(
                        "[dbo].[SP_DeleteCompany]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
