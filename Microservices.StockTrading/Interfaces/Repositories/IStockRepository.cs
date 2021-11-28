using Microservices.StockTrading.DTOs;
using Microservices.StockTrading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> GetAsync(Guid id);

        Task<IEnumerable<Stock>> GetAsync(IList<Guid> ids);

        Task<Stock> GetAsync(Expression<Func<Stock, bool>> expression);

        Task<Stock> AddAsync(Stock entity);


        IQueryable<Stock> Query();

        Task<IList<Stock>> GetAllAsync(Expression<Func<Stock, bool>> expression);

        public Task<IList<UserStockDto>> GetUserStocks(Guid userId);

        public Task<UserStock> BuyStock(UserStock userStock);

    }
}

