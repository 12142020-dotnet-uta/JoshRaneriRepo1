using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLib
{
    public class Product
    {
        public Product()
        {
            
        }
        private int productId;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ProductId { get => productId; set => productId = value; }
        private decimal price;
        [Required]
        public decimal Price { get => price; set => price = value;}
        private string description;
        [Required]
        public string Description { get => description; set => description = value; }
    }
}