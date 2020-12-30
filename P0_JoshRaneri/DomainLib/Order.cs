using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class Order
    {
        public Order()
        {
            
        }
        private Guid orderId = Guid.NewGuid();
        [Key]
        public Guid OrderId { get => orderId; set => orderId = value; }
        private int locationId;
        [ForeignKey("LocationId")]
        public int LocationId { get => locationId; set => locationId = value; }
        private int customerId;
        [ForeignKey("CustomerId")]
        public int CustomerId { get => customerId; set => customerId = value; }
        private Guid cartId;
        [ForeignKey("CartId")]
        public Guid CartId { get => cartId; set => cartId = value; }
        private DateTime orderTime;
        [Required]
        public DateTime OrderTime { get => orderTime; set => orderTime = value; }
        private decimal orderTotal;
        public decimal OrderTotal { get => orderTotal; set => orderTotal = value; }
    }
}