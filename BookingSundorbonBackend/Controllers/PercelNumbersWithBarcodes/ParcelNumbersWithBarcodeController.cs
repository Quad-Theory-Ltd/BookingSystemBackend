﻿using BookingSundorbon.Features.Repositories.ParcelNumbersWithBarcodeRepository;
using BookingSundorbon.Features.Repositories.ProductTypeRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookingSundorbonBackend.Controllers.PercelNumbersWithBarcodes
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParcelNumbersWithBarcodeController : ControllerBase
    {
        private readonly IParcelNumbersWithBarcodeRepository _numbersWithBarcodeRepository;

        public ParcelNumbersWithBarcodeController(IParcelNumbersWithBarcodeRepository numbersWithBarcodeRepository)
        {
            _numbersWithBarcodeRepository = numbersWithBarcodeRepository;
        }
        [HttpGet("ParcelBarcodes")]
        public async Task<IActionResult> GetAllParcelNumberrsWithBarcodes()
        {
            var parcelNumberrsWithBarcodes = await _numbersWithBarcodeRepository.GetAllParcelNumbersWithBarcodes();
            return Ok(parcelNumberrsWithBarcodes);
        }

        [HttpGet("AgentParcelBarcodes/{userId}")]
        public async Task<IActionResult> GetAgentParcelNumberrsWithBarcodes(int userId)
        {
            var parcelNumberrsWithBarcodes = await _numbersWithBarcodeRepository.GetAgentParcelNumberrsWithBarcodes(userId);
            return Ok(parcelNumberrsWithBarcodes);
        }
    }
    
    
}
