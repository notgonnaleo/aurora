using Backend.Domain.Entities.Authentication.Users;
using Backend.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Users
{
    public class UserService
    {
        private readonly AuthDbContext _authDbContext;
        public UserService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return _authDbContext.Users.Where(x => x.Active == true).ToList();
        }

        public async Task<IEnumerable<User>> GetById(Guid Id)
        {
            return _authDbContext.Users.Where(x => x.Id == Id && x.Active == true).ToList();
        }

        public async Task<IEnumerable<User>> GetByUserName(string UserName)
        {
            return _authDbContext.Users.Where(x => x.Username == UserName && x.Active == true).ToList();
        }
    }
}
