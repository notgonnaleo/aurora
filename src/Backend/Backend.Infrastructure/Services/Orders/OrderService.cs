using Backend.Infrastructure.Context;
using Backend.Infrastructure.Services.Authorization;
using Backend.Infrastructure.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services.Orders
{
    public class OrderService : Service
    {
        private readonly AppDbContext _appDbContext;
        public OrderService(UserContextService userContextService, AppDbContext appDbContext) : base(userContextService)
        {
            _appDbContext = appDbContext;
        }
    }
}
