
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microservices.Authentication.Models;
using Microservices.Authentication.Identity;
using MediatR;

namespace Microservices.Authentication.Extensions
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
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<Role>, RoleStore>();
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
