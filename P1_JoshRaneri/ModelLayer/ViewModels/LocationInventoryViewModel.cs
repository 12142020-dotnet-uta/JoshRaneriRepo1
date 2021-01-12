using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    //[BindProperties(SupportsGet = true)]
    public class LocationInventoryViewModel
    {
        public Location Location { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Product Description")]
        public string Description { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Quantity On Hand")]
        public int Quantity { get; set; }
        [DisplayName("Purchase Quantity")]
        public int purchaseQuantity { get; set; }
    }

}
