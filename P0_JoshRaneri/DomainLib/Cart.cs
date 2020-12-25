using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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
        private int locationId;
        [Required]
        public int LocationId { get => locationId; set => locationId = value; }
        List<InventoryLine> cartList = new List<InventoryLine>();
        public List<InventoryLine> CartList { get => cartList; set => cartList = value; }
    }
}