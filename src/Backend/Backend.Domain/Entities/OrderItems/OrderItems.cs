using Backend.Domain.Entities.Base;
using Backend.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.OrderItems.Request;
using Backend.Domain.Entities.Products;

namespace Backend.Domain.Entities.OrderItems
{
    [Table("OrderItems")]
    public class OrderItem : Model
    {
        public OrderItem() { }
        public OrderItem(OrderItemsRequest orderItemsRequest, decimal totalWeight, decimal unitValue, int itemQuantity)
        {
            TenantId = orderItemsRequest.TenantId;
            OrderId = orderItemsRequest.OrderId;
            OrderItemId = Guid.NewGuid();
            ProductId = orderItemsRequest.ItemId;
            VariantId = orderItemsRequest.ItemVariantId.GetValueOrDefault();
            ItemQuantity = itemQuantity;
            ItemUnitAmount = unitValue;
            ItemTotalWeight = totalWeight * itemQuantity;
            ItemTotalAmount = unitValue * itemQuantity;
            Active = true;
        }

        public OrderItem(OrderItemsRequest orderItemsRequest)
        {
            TenantId = orderItemsRequest.TenantId;
            OrderId = orderItemsRequest.OrderId;
            OrderItemId = Guid.NewGuid();
            ProductId = orderItemsRequest.ItemId;
            VariantId = orderItemsRequest.ItemVariantId.GetValueOrDefault();
            ItemQuantity = orderItemsRequest.ItemQuantity;
            Active = true;
        }

        [Required]
        public Guid TenantId { get; set; }
        [Key]
        public Guid OrderItemId { get; set; }
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public Guid? VariantId { get; set; }
        public decimal? ItemTotalWeight { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemUnitAmount { get; set; }
        public decimal ItemTotalAmount { get; set; }

        public Order? Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
