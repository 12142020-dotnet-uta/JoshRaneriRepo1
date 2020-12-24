using System;
using System.ComponentModel.DataAnnotations;

namespace P0_JoshRaneri
{
    public class Product
    {
        private Guid productId = Guid.NewGuid();
        [Key]
        public Guid ProductId { get { return ProductId; } set { ProductId = value; } }
    }
}