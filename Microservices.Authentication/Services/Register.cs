
using MediatR;
using Microservices.Authentication.Exceptions;
using Microservices.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Microservices.Authentication.Services
{
    public static class Register
    {
        public class Command: IRequest<Result>
        {

            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class Result : BaseResponse
        {

        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly Context _context;
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly UserManager<User> _userManager;

            public Handler(Context context, UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _userManager = userManager;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var userNameExists = await _context.Users.AnyAsync(m => m.Email == request.Email, cancellationToken);     

                if (userNameExists)
                {
                    throw new BadRequestException(ResponseMessages.EmailAlreadyExistError);
                }

                var salt = Guid.NewGuid().ToString();
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    HashSalt = salt
                };
                var passwordHash = _passwordHasher.HashPassword(user, $"{request.Password}{salt}");
                user.PasswordHash = passwordHash;

                _context.Entry(user).State = EntityState.Added;
                await _context.SaveChangesAsync(cancellationToken);
                return new Result
                {
                    Status = true,
                    Message = ResponseMessages.Successful
                };
            }
        }
    }
}
