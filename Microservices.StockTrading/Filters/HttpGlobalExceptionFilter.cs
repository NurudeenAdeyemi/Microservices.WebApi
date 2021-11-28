using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microservices.StockTrading.ActionResults;
using Microservices.StockTrading.Exceptions;
using Microservices.StockTrading.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservices.StockTrading.Filters
{
    public class HttpGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new NotFoundObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception.GetType() == typeof(BadRequestException))
            {
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new BadRequestObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception.GetType() == typeof(ConflictException))
            {
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new ConflictObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (context.Exception.GetType() == typeof(BadGatewayException))
            {
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new BadGatewayObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            else
            {
                var response = new BaseResponse
                {
                    Message = "An error occurred. Try it again."
                };

                if (_env.IsDevelopment())
                {
                    response.Message = context.Exception.StackTrace;
                }

                context.Result = new InternalServerErrorObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
