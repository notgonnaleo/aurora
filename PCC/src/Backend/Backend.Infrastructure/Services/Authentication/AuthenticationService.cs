using Backend.Domain.Entities.Authentication.Users;
using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Authentication
{
    public class AuthenticationService
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserService _userService;
        public AuthenticationService(AuthDbContext authDbContext, UserService userService)
        {
            _authDbContext = authDbContext;
            _userService = userService;
        }
        // Create JWT methods and stuff 

        public async Task<bool> Login() // Figure out how can i return the token and boolean flag
        {
            return true;
        }

    }
}
