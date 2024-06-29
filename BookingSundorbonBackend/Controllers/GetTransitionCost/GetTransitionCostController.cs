using BookingSundorbon.Features.Repositories.BranchRepository;
using BookingSundorbon.Features.Repositories.GetTransitionCostRepository;
using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.GetTransitionCost
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTransitionCostController : ControllerBase
    {
        private readonly IGetTransitionCostRepository _getTransitionCostRepository;

        public GetTransitionCostController (IGetTransitionCostRepository getTransitionCostRepository)
        {
            _getTransitionCostRepository = getTransitionCostRepository;
        }


        [HttpGet]
        public Task<decimal> GetTotalTransitionCostWithoutVat([FromQuery]GetTransitionCostView getTransitionCost)
        {
            var cost = _getTransitionCostRepository.GetTransitionCostWithoutVat(getTransitionCost);
          
            return cost;
        }
    }
}
