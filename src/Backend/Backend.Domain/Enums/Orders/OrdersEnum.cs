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
        [Description("Em aberto")]
        Open = 1,
        [Description("Em andamento")]
        InProgress = 2,
        [Description("Parcialmente finalizado")]
        PartiallyDone = 3,
        [Description("Finalizado")]
        Done = 4,
        [Description("Em atraso")]
        Overdue = 5,
        [Description("Cancelado")]
        Canceled = 6,
        [Description("Reembolsado")]
        Refunding = 7,
    }
}
