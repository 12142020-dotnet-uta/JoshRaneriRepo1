using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class LocationInventory
    {
        public LocationInventory()
        {
        }
        public LocationInventory(int locId, int prodId, int quant)
        {
            this.LocationId = locId;
            this.ProductId = prodId;
            this.Quantity = quant;
        }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
