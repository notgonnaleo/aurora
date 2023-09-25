using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.UserRoutes
{
    public class UserRoute
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public bool Access { get; set; }
    }
}
