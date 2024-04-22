using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Orders.Request
{
    public class OrderRequest
    {
        public Guid SellerId { get; set; }
        public Guid PayerId { get; set; }
        public int PaymentTypeId { get; set; }
        public int ParcelsQuantity { get; set; }
    }
}
