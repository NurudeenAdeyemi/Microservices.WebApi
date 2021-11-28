using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Authentication.Models
{
    public class UserAuth
    {
        public class LoginRequestModel
        {
            [EmailAddress]
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class LoginResponseModel : BaseResponse
        {
            public LoginResponseData Data { get; set; }
        }

        public class LoginResponseData
        {
            public string Email { get; set; }

            public string UserName { get; set; }
        }
    }
}
