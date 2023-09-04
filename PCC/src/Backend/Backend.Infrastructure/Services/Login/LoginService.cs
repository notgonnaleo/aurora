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
    public class LoginService
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public LoginService(AuthDbContext authDbContext, UserService userService, IConfiguration configuration)
        {
            _authDbContext = authDbContext;
            _userService = userService;
            _configuration = configuration;
        }

        public LoginResponse Authenticate(LoginRequest request)
        {
            LoginResponse user = _authDbContext.Users.Where(x => x.Username == request.Username && x.Password == request.Password)
                .Select(x => new LoginResponse
                {
                    Id = x.Id,
                    Username = x.Username,
                    Password = x.Password,
                })
                .First();

            user.Token = GenerateToken(user);
            return user; // Returning the user basic info + token to authorize api request (TODO: Add claims on it tho)
        }
        // Things looks good yet

        // Need to review this token generate method
        private string GenerateToken(LoginResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWTKeySettings:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
