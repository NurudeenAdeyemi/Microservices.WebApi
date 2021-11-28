using MediatR;
using Microservices.Authentication.Filters;
using Microservices.Authentication.Models;
using Microservices.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Microservices.Authentication.Models.UserAuth;

namespace Microservices.Authentication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, IIdentityService identityService, IConfiguration configuration, IMediator mediator, ILogger<AuthController> logger)
        {
            _identityService = identityService;
            _configuration = configuration;
            _mediator = mediator;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("{register}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Register.Result))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Register(Register.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("token")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, $"{model.Password + user.HashSalt}");
                if (result)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var token = _identityService.GenerateToken(user, roles);
                    var response = new LoginResponseModel
                    {
                        Message = ResponseMessages.Successful,
                        Status = true,
                        Data = new LoginResponseData
                        {
                            UserName = user.Email,
                            Email = user.Email,
                           
                        }
                    };
                    var expiry = DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetValue<string>("JwtTokenSettings:TokenExpiryPeriod")));
                    Response.Headers.Add("Token", token);
                    Response.Headers.Add("TokenExpiry", expiry.ToUnixTimeMilliseconds().ToString());
                    Response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
                    return Ok(response);
                }
            }
            return BadRequest(new BaseResponse { Status = false, Message = "Invalid Credentials" });
        }
    }
}
