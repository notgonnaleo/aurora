using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Enums.EnumExtensions;

namespace Backend.Domain.Enums.Orders
{
    public enum OrdersStatusEnums
    {
        [Description("Open")]
        Open = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Partially Done")]
        PartiallyDone = 3,
        [Description("Done")]
        Done = 4,
        [Description("Overdue")]
        Overdue = 5,
        [Description("Canceled")]
        Canceled = 6,
        [Description("Refunding")]
        Refunding = 7,
    }
}
