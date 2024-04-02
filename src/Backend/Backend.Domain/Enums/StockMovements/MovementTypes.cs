using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Enums.EnumExtensions;

namespace Backend.Domain.Enums.StockMovements.MovementType
{
    public enum MovementTypes
    {
        [Description("Output")]
        Output = 0,
        [Description("Input")]
        Input = 1
    }
}

