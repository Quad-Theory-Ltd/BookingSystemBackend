﻿using BookingSundorbon.Features.Repositories.BranchRepository;
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
        public async Task<ActionResult> GetTotalTransitionCost([FromQuery]GetTransitionCostView getTransitionCost)
        {
            try
            {
                if (getTransitionCost == null)
                {
                    return BadRequest();
                }
                var result = await _getTransitionCostRepository.GetTransitionCost(getTransitionCost);

                return Ok(result);
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}
