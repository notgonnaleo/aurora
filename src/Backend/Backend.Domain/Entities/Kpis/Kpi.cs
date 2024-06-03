using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Kpis
{
    public class DashboardAnalytics
    {
        public int? TotalProducts { get; set; }
        public int? TotalCustomers { get; set; }
        public int? TotalOpenOrders { get; set; }
        public int? TotalClosedOrders { get; set; }
    }
}
