using BookingSundorbon.Features.Repositories.CompanyRepository;
using BookingSundorbon.Views.DTOs.CompanyView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyView company)
        {
            if (company == null)
            {
                return BadRequest("Company is null.");
            }

            var newCompanyId = await _companyRepository.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = newCompanyId }, newCompanyId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompanyAsync(id);
            if (company == null)
            {
                return NotFound("Company not found.");
            }
            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyView company)
        {
            if (company == null || company.Id != id)
            {
                return BadRequest("Company data is invalid.");
            }

            var existingCompany = await _companyRepository.GetCompanyAsync(id);
            if (existingCompany == null)
            {
                return NotFound("Company not found.");
            }

            await _companyRepository.UpdateCompanyAsync(company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var existingCompany = await _companyRepository.GetCompanyAsync(id);
            if (existingCompany == null)
            {
                return NotFound("Company not found.");
            }

            await _companyRepository.DeleteCompanyAsync(id);
            return NoContent();
        }

    }
}
