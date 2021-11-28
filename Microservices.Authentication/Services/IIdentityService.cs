using Microservices.Authentication.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Authentication.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GenerateToken(User user, IEnumerable<string> roles);

        JwtSecurityToken GetClaims(string token);

        string GetClaimValue(string type);

        string GenerateSalt();
    }
}
