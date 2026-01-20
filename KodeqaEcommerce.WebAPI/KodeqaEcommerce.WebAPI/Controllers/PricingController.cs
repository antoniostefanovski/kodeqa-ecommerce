using KodeqaEcommerce.WebAPI.Models;
using KodeqaEcommerce.WebAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodeqaEcommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService pricingService;

        public PricingController(IPricingService pricingService)
        {
            this.pricingService = pricingService;
        }

        [HttpGet("calculate")]
        [ProducesResponseType(typeof(PricingResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CalculatePricing([FromQuery] OrderRequest orderRequest)
        {
            var response = pricingService.CalculatePrice(orderRequest);

            return Ok(response);
        }
    }
}
