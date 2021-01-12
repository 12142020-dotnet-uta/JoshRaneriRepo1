using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Order
    {
        public Order()
        {

        }
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        [ForeignKey("Id")]
        public string Id { get; set; }
        [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotal { get; set; }
    }
}
