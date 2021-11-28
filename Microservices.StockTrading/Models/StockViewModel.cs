using Microservices.StockTrading.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Models
{
    public class CreateStockRequestModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }


    public class StockResponseModel : BaseResponse
    {
        public StockDto Data { get; set; }
    }

    public class StocksResponseModel : BaseResponse
    {
        public IEnumerable<StockDto> Data { get; set; } = new List<StockDto>();
    }
}
