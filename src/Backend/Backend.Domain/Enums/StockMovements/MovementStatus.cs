﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Enums.EnumExtensions;

namespace Backend.Domain.Enums.StockMovements
{
    public enum MovementStatus
    {
        [Description("Disponível")]
        Available = 1,
        [Description("Em falta")]
        OutOfStock = 2,
        [Description("Reserved")]
        Reserved = 3
    }


}
