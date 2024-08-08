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
        

    }
}
