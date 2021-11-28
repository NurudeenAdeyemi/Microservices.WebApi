using Microservices.StockTrading.Filters;
using Microservices.StockTrading.Interfaces.Services;
using Microservices.StockTrading.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockService stockService, ILogger<StockController> logger)
        {
            _stockService = stockService;
            _logger = logger;

        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateStockRequestModel))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestModel model)
        {
            var response = await _stockService.AddStock(model);
            return Ok(response);
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetStock([FromRoute] Guid id)
        {
            var response = await _stockService.GetStock(id);
            return Ok(response);
        }

        [HttpPost("buystock")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BuyStockRequestModel))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> BuyStock([FromBody] BuyStockRequestModel model, Guid userId)
        {
            var response = await _stockService.BuyStock(model, userId);
            return Ok(response);
        }

        //[Authorize]
        [HttpGet("user/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetUserStocks([FromRoute] Guid userId)
        {
            var response = await _stockService.GetUserStocks(userId);
            return Ok(response);
        }
    }
}
