using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Products;

namespace Backend.Domain.Entities.OrderItems.Response
{
    public class OrderItemsResponse
    {
        public Guid TenantId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public ProductDetail Item { get; set; }
        public ProductVariant? ItemVariant { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemUnitAmount { get; set; } 
        public decimal ItemTotalAmount { get; set; }
    }
}
