using Microservices.StockTrading.DTOs;
using Microservices.StockTrading.Exceptions;
using Microservices.StockTrading.Interfaces;
using Microservices.StockTrading.Interfaces.Services;
using Microservices.StockTrading.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
      
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;

        }
        public async Task<BaseResponse> AddStock(CreateStockRequestModel model)
        {
            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price
            };
           
            await _stockRepository.AddAsync(stock);


            return new BaseResponse
            {
                Status = true,
                Message = "Stock added successfully."
            };
        }

        public async Task<StockResponseModel> GetStock(Guid id)
        {
            var stock = await _stockRepository.Query()
                .SingleOrDefaultAsync(u => u.Id == id);

            return new StockResponseModel
            {
                Data = new StockDto
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    Price = stock.Price

                },
                Status = true,
                Message = "Stock retrieved Successfully"

            };
        }

        public async Task<StocksResponseModel> GetStocks()
        {
            var stocks = await _stockRepository.Query()
                .Select(b => new StockDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price
                }).ToListAsync();

            return new StocksResponseModel
            {
                Data = stocks,
                Status = true,
                Message = "Stocks retrieved successfully"
            };
        }

        public async Task<UserStocksResponseModel> GetUserStocks(Guid userId)
        {
            //bool bookReturned = false;
            var userStocks = await _stockRepository.GetUserStocks(userId);

            return new UserStocksResponseModel
            {
                Data = userStocks,
                Status = true,
                Message = "User Stocks retrieved successfully"

            };
        }

        public async Task<BaseResponse> BuyStock(BuyStockRequestModel model, Guid userId)
        {
            var stock = await _stockRepository.GetAsync(model.StockId);
            if (stock == null)
            {
                throw new NotFoundException($"Stock with id {model.StockId} does not exist");
            }

            var userStock = new UserStock
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StockId = stock.Id,
                Price = stock.Price,
                Share = model.Share,
                Total = model.Share * stock.Price
                
            };
            await _stockRepository.BuyStock(userStock);

            return new BaseResponse
            {

                Status = true,
                Message = $"Stock: {stock.Name}  of {userStock.Share} bought successfully"
            };
        }
    }
}
