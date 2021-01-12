using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Mapper
    {
        public UserViewModel ConvertUserToViewModel(CustomUser user, Location location)
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                DefaultStore = location
            };
            return userViewModel;
        }
        public LocationInventoryViewModel CreateLocationInventoryViewModel(LocationInventory locationInventory, Location location, Product product)
        {
            LocationInventoryViewModel livm = new LocationInventoryViewModel()
            {
                Location = location,
                ProductId = product.ProductId,
                Description = product.Description,
                Price = product.Price,
                Quantity = locationInventory.Quantity,
            };
            return livm;
        }
    }
}
