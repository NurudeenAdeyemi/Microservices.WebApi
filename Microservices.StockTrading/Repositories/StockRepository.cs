using Microservices.StockTrading.DTOs;
using Microservices.StockTrading.Interfaces;
using Microservices.StockTrading.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microservices.StockTrading.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly Context _context;
        public StockRepository(Context context)
        {
            _context = context;
        }
       
        public async Task<Stock> AddAsync(Stock entity)
        {
            await _context.Stocks
                .AddAsync(entity);
             await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<Stock>> GetAllAsync(Expression<Func<Stock, bool>> expression)
        {
            return await _context.Stocks
                .Where(expression).ToListAsync();
        }

        public async Task<Stock> GetAsync(Guid id)
        {
            return await _context.Stocks.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Stock>> GetAsync(IList<Guid> ids)
        {
            return await _context.Stocks
                .Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<Stock> GetAsync(Expression<Func<Stock, bool>> expression)
        {
            return await _context.Stocks.FirstOrDefaultAsync(expression);
        }

        public IQueryable<Stock> Query()
        {
            return _context.Stocks
               .AsQueryable();
        }

        public async Task<UserStock> BuyStock(UserStock userStock)
        {
            await _context.UserStocks.AddAsync(userStock);
            await _context.SaveChangesAsync();
            return userStock;
        }

        public async Task<IList<UserStockDto>> GetUserStocks(Guid userId)
        {
            return await _context.UserStocks
                .Include(b => b.Stock)
                 .Where(b => b.UserId == userId).Select(b => new UserStockDto
                 {
                     Id = b.StockId,
                     Name = b.Stock.Name,
                     Price = b.Price,
                     Share = b.Share,
                     Total = b.Total
                     
                 }).ToListAsync();
        }
    }
}
