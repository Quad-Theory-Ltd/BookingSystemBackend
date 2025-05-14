using BookingSundorbon.Features.Repositories.SubBranchRepository;
using BookingSundorbon.Views.DTOs.SubBranchView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.SubBranch
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubBranchController : ControllerBase
    {

        private readonly ISubBranchRepository _subBranchRepository;

        public SubBranchController(ISubBranchRepository subBranchRepository)
        {
            _subBranchRepository = subBranchRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveSubBranchs()
        {
            var subBranch = await _subBranchRepository.GetAllActiveSubBranchsAsync();
            return Ok(subBranch);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubBranch([FromBody] SubBranchCreateView subBranch)
        {
            if (subBranch == null)
            {
                return BadRequest("SubBranch is Null");
            }
            var subBranchId = await _subBranchRepository.CreateSubBranchAsync(subBranch);

            if(subBranchId == -1)
            {
                return BadRequest("AgentId or Employee Id Required");
            }

            return CreatedAtAction(nameof(GetSubBranch), new { id = subBranchId }, subBranchId);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetSubBranch(int id)
        {
            var subBranch = await _subBranchRepository.GetSubBranchAsync(id);
            if (subBranch == null)
            {
                return NotFound("SubBranch not found.");
            }
            return Ok(subBranch);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubBranch(int id, [FromBody] SubBranchView subBranch)
        {
            if (subBranch == null || subBranch.Id != id)
            {
                return BadRequest(" SubBranch Id is Invalid!");
            }
            var existingSubBranch = await _subBranchRepository.GetSubBranchAsync(id);
            if (existingSubBranch == null)
            {
                return BadRequest(" SubBranch Not Found!");
            }
            await _subBranchRepository.UpdateSubBranchAsync(subBranch);
            return NoContent();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSubBranch(int id)
        //{
        //    var subBranch = await _subBranchRepository.GetSubBranchAsync(id);
        //    if (subBranch == null)
        //    {
        //        return NotFound("SubBranch not found.");
        //    }

        //    await _subBranchRepository.DeleteSubBranchAsync(id);
        //    return NoContent();
        //}

    }
}
