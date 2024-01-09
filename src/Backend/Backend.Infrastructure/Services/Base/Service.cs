using Backend.Domain.Entities.Authentication.Users.UserContext;
using Backend.Infrastructure.Services.Authentication;
using Backend.Infrastructure.Services.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Base
{
    public class Service
    {
        private readonly UserContextService _userContextService;
        public Service(UserContextService userContextService)
        {
            _userContextService = userContextService;
        }
        public UserSessionContext LoadContext()
        {
            return _userContextService.LoadContext();
        }
    }
}
