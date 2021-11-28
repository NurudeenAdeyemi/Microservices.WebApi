
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Microservices.StockTrading.Models;
using Microservices.StockTrading.Interfaces;
using Microservices.StockTrading.Repositories;
using Microservices.StockTrading.Interfaces.Services;
using Microservices.StockTrading.Services;

namespace Microservices.StockTrading.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static  IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
   
            services.AddScoped<IStockRepository, StockRepository>();
         
            services.AddScoped<IStockService, StockService>();
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
