using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Authentication.Users.Login.Request;
using Backend.Domain.Entities.Authentication.Users.Login.Response;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Authentication
{
    public class AuthenticationService
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public AuthenticationService(AuthDbContext authDbContext, UserService userService, IConfiguration configuration)
        {
            _authDbContext = authDbContext;
            _userService = userService;
            _configuration = configuration;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            User? user = _authDbContext.Users.Where(x => x.Username == request.Username && x.Password == request.Password)
                .FirstOrDefault();

            LoginResponse response = new LoginResponse();
            if (user != null)
            {
                response = new LoginResponse()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Token = GenerateToken(user),
                    Success = true,
                    Message = "Logged in"
                };
            }
            else
            {
                response = new LoginResponse()
                {
                    Id = null,
                    Username = "",
                    Password = "",
                    Token = "",
                    Success = false,
                    Message = "User does not exists or credentials are wrong."
                };
            }
            return response; // Returning the user basic info + token to authorize api request (TODO: Add claims on it tho)
        }
        // Things looks good yet

        // Need to review this token generate method
        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTKeySettings:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "https://localhost:7299/", // Application that will generate the token
                Audience = "https://localhost:7299/", // Application that will consume it. (Can be a list of APIs that will use it)
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
