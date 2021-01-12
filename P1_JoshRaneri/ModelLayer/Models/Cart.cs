using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Cart
    {
        public Cart()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid CartId { get; set; } = Guid.NewGuid();
        [ForeignKey("Id")]
        public string Id { get; set; }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
    }
}
