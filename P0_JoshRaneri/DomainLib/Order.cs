using System;
using System.ComponentModel.DataAnnotations;

namespace P0_JoshRaneri
{
    public class Order
    {
        private Guid orderId = Guid.NewGuid();
        [Key]
        public Guid OrderId { get { return orderId; } set { orderId = value; } }
        
    }
}