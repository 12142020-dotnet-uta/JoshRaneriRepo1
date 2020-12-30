using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class InventoryLine
    {
        public InventoryLine()
        {
        }
        public InventoryLine(int locId, int prodId, int quant)
        {
            this.LocationId = locId;
            this.ProductId = prodId;
            this.Quantity = quant;
        }
        private int locationId;
        [ForeignKey("LocationId")]
        public int LocationId { get => locationId; set => locationId = value; }        
        private int productId;
        [ForeignKey("ProductId")]
        public int ProductId { get => productId; set => productId = value; }
        private int quantity;
        [Required]
        public int Quantity { get => quantity; set => quantity = value; }
    }
}