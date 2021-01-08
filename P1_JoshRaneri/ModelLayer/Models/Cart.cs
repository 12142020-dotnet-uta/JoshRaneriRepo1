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
        public Guid CartId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
