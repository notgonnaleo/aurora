using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.Orders
{
    public enum OrdersStatusEnums
    {
        Open = 1,
        InProgress = 2,
        PartiallyDone = 3,
        Done = 4,
        Overdue = 5,
        Canceled = 6,
        Refunding = 7,
    }
}
