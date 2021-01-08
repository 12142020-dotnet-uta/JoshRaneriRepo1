using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class CartItem
    {
        public CartItem()
        {
        }
        private Guid cartId;
        [ForeignKey("CartId")]
        [Required]        
        public Guid CartId { get => cartId; set => cartId = value; }
        private int productId;
        [ForeignKey("ProductId")]
        [Required]        
        public int ProductId { get => productId; set => productId = value; }
        private int cartQuantity;
        public int CartQuantity { get => cartQuantity; set => cartQuantity = value; }
    }
}