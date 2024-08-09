using BookingSundorbon.Features.Repositories.AgentRequisitionRepository;
using BookingSundorbon.Features.Repositories.IssueRepository;
using BookingSundorbon.Features.Repositories.IssueRepository;
using BookingSundorbon.Views.DTOs.IssueView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.Issue
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueRepository _issueRepository;

        public IssueController(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
     


        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] IssueView issue)
        {
            if (issue == null)
            {
                return BadRequest("Issue is Null");
            }
            var issueId = await _issueRepository.CreateIssueAsync(issue);

            //return CreatedAtAction(nameof(GetIssue), new { id = issueId }, issueId);
            return Ok("Issue Created");
        }

        [HttpGet("GetAllIssue")]
        public async Task<IActionResult> GetAllIssue()
        {
            var issues = await _issueRepository.GetAllIssueAsync();
            return Ok(issues);
        }

        [HttpGet("{issueNo}")]
        public async Task<IActionResult> GetIssueByIssueNo(int issueNo)
        {
            var agent = await _issueRepository.GetIssueAsync(issueNo);
            if (agent == null)
            {
                return NotFound("Agent Requisition not found.");
            }
            return Ok(agent);
        }

        [HttpGet("GetAllIssueNo")]

        public async Task<IActionResult> GetAllIssueNo()
        {
            var issueNo = await _issueRepository.GetAllIssueNo();
            if (issueNo == null)
            {
                return NotFound("Issue No not found.");
            }
            return Ok(issueNo);
        }

        [HttpGet("GetNextIssueNo")]
        public async Task<IActionResult> GetNextIssueNo()
        {
            var issueNo = await _issueRepository.GetNextIssueNoAsync();
            if (issueNo == 0)
            {
                return NotFound("Issue No Not found.");
            }
            return Ok(issueNo);
        }



    }
}
