using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class Cart
    {
        public Cart()
        {

        }
        private Guid cartId = Guid.NewGuid();
        [Key]
        public Guid CartId { get => cartId; set => cartId = value; }
        [ForeignKey("CustomerId")]
        private int customerId;
        public int CustomerId { get => customerId; set => customerId = value; }
    }
}