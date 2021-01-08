using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class OrderItem
    {
        public OrderItem()
        {
        }
        private Guid orderId;
        [ForeignKey("OrderId")]
        [Required]        
        public Guid OrderId { get => orderId; set => orderId = value; }
        private int productId;
        [ForeignKey("ProductId")]
        [Required]        
        public int ProductId { get => productId; set => productId = value; }
        private int orderQuantity;
        public int OrderQuantity { get => orderQuantity; set => orderQuantity = value; }
    }
}