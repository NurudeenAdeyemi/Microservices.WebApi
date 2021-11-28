using Microservices.StockTrading.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Models
{
    public class BuyStockRequestModel
    {
       // public Guid UserId { get; set; }
        public Guid StockId { get; set; }

        public int Share { get; set; }

    }

    
    public class UserStockResponseModel : BaseResponse
    {
        public UserStockDto Data { get; set; }
    }

    public class UserStocksResponseModel : BaseResponse
    {
        public IEnumerable<UserStockDto> Data { get; set; } = new List<UserStockDto>();
    }
}
