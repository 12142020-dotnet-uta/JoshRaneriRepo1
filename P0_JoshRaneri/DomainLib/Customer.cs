using System;
using System.ComponentModel.DataAnnotations;

namespace P0_JoshRaneri
{
    public class Customer
    {
        private Guid customerId = Guid.NewGuid();
        [Key]
        public Guid CustomerId { get { return customerId; } set { customerId = value; } }

    }
}