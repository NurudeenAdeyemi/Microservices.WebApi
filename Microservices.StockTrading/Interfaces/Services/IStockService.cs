using Microservices.StockTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Interfaces.Services
{
    public interface IStockService
    {
        Task<BaseResponse> AddStock(CreateStockRequestModel model);
        Task<StockResponseModel> GetStock(Guid id);
        public Task<BaseResponse> BuyStock(BuyStockRequestModel model, Guid userId);
        public Task<UserStocksResponseModel> GetUserStocks(Guid userId);
    }
}
