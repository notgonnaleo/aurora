using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.UserRoutes
{
    public class UserRoute
    {
        public Guid? RouteId { get; set; }
        public int? RouteCode { get; set; }
        public string RouteName { get; set; }
        public bool Access { get; set; }
    }
}
