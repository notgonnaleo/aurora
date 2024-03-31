using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.StockMovements
{
    public enum MovementStatus
    {
        [Description("Available")]
        Available = 1,
        [Description("Out of Stock")]
        OutOfStock = 2,
        [Description("Reserved")]
        Reserved = 3
    }


}
