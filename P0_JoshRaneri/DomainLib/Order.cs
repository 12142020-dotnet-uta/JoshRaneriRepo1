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
        private int orderId;
        [Key]        
        public int OrderId { get => orderId; set => orderId = value; }
        private int locationId;
        [ForeignKey("LocationId")]
        public int LocationId { get => locationId; set => locationId = value; }
        private int customerId;
        [ForeignKey("CustomerId")]
        public int CustomerId { get => customerId; set => customerId = value; }
        private List<Product> orderProducts;
        [Required]
        public List<Product> OrderProducts { get => orderProducts; set => orderProducts = value; }
        private DateTime orderTime;
        [Required]
        public DateTime OrderTime { get => orderTime; set => orderTime = value; }
    }
}