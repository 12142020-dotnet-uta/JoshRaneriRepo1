using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class OrderInventory
    {
        public OrderInventory()
        {
        }
        [ForeignKey("OrderId")]
        [Required]
        public Guid OrderId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
    }
}
