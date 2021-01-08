using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CartInventory
    {
        public CartInventory()
        {
        }
        [ForeignKey("CartId")]
        [Required]
        public Guid CartId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public int ProductId { get; set; }
        public int CartQuantity { get; set; }
    }
}
