using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Views.DTOs.BranchView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Branch
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] BranchView branch)
        {
            if (branch == null)
            {
                return BadRequest("Branch is null.");
            }

            var newBranchId = await _branchRepository.CreateBranchAsync(branch);
            return CreatedAtAction(nameof(GetBranch), new { id = newBranchId }, newBranchId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            var branch = await _branchRepository.GetBranchAsync(id);
            if (branch == null)
            {
                return NotFound("Branch not found.");
            }
            return Ok(branch);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var companies = await _branchRepository.GetAllBranchesAsync();
            return Ok(companies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] BranchView branch)
        {
            if (branch == null || branch.Id != id)
            {
                return BadRequest("Branch data is invalid.");
            }

            var existingBranch = await _branchRepository.GetBranchAsync(id);
            if (existingBranch == null)
            {
                return NotFound("Branch not found.");
            }

            await _branchRepository.UpdateBranchAsync(branch);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var existingBranch = await _branchRepository.GetBranchAsync(id);
            if (existingBranch == null)
            {
                return NotFound("Branch not found.");
            }

            await _branchRepository.DeleteBranchAsync(id);
            return NoContent();
        }

    }
}
