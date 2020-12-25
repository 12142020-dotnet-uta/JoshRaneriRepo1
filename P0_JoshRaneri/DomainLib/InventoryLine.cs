using System.ComponentModel.DataAnnotations;

namespace DomainLib
{
    public class InventoryLine
    {
        public InventoryLine()
        {
        }
        private int locationId;
        [Key]
        [Required]
        public int LocationId { get => locationId; set => locationId = value; }        
        private int productId;
        [Key]
        [Required]
        public int ProductId { get => productId; set => productId = value; }
        private int quantity;
        [Required]
        public int Quantity { get => quantity; set => quantity = value; }
    }
}