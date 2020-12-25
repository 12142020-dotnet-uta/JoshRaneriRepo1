using System;
using System.Collections.Generic;
using System.Linq;
using DomainLib;

namespace DBAccessLib
{
    public class RepositoryLayer
    {
        // public List<Customer> Customers { get; set; }
        // public List<Order> Orders { get; set; }
        // public List<Location> Locations { get; set; }
        // public List<Product> Products { get; set; }
        // public List<InventoryLine> InventoryLines { get; set; }
        P0_DbContext DbContext = new P0_DbContext();
        public bool loggedIn = false;
        public bool storeSelected = false;
        bool userFound = false;        
        private Customer currentCustomer = new Customer();
        public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }
        private Cart currentCart = new Cart();
        public Cart CurrentCart { get => currentCart; set => currentCart = value; }
        private List<Location> storeList = new List<Location>();
        public List<Location> StoreList { get => storeList; set => storeList = value; }
        private List<Product> storeProductList = new List<Product>();
        public List<Product> StoreProductList { get => storeProductList; set => storeProductList = value; }
        int currentStore = 0;
        public void NewCustomer(string userName, string password, string firstName, string lastName, string address, int defaultStore)
        {
            foreach (Customer checkCustomer in DbContext.Customers)
            {
                if (checkCustomer.UserName == userName)
                {
                    Console.WriteLine("User already exists. Please login or choose a new User name.");
                    userFound = true;
                }
            }
            if (userFound == false)
            {
                Customer newCust = new Customer(userName, password, firstName, lastName, address, defaultStore);
                DbContext.Customers.Add(newCust);
                DbContext.SaveChanges();
                Console.WriteLine("\nUser successfully created. Please log in.\n");
            }
        }
        public void CustomerLogin(string userName, string password)
        {
            foreach (Customer checkCustomer in DbContext.Customers)
            {
                if (checkCustomer.UserName == userName && checkCustomer.Password == password)
                {
                    loggedIn = true;
                    currentCustomer = checkCustomer;
                    if (currentCustomer.DefaultStore != 0)
                    {
                        currentStore = currentCustomer.DefaultStore;
                    }
                }
                else
                {
                    loggedIn = false;
                    Console.WriteLine("Invalid Username/Password.");
                }
            }
        }
        public void NewOrder()
        {

        }
        public void PlaceOrder()
        {

        }
        public void AddToCart(int product, int quantity)
        {
            foreach (InventoryLine line in DbContext.InventoryLines)
            {
                if (line.LocationId == currentStore && line.ProductId == product && line.Quantity >= quantity)
                {
                    InventoryLine lineIn = new InventoryLine();
                    lineIn.LocationId = currentStore;
                    lineIn.ProductId = product;
                    lineIn.Quantity = quantity;
                    currentCart.CartList.Add(lineIn);
                    line.Quantity -= quantity;                    
                }
                else if (line.LocationId == currentStore && line.ProductId == product && line.Quantity < quantity)
                {
                    Console.WriteLine("Location does not have enough of that item.");
                }
            }
        }
        public void ChangeCartQuantity(int product, int quantity)
        {
            foreach (InventoryLine line in currentCart.CartList)
            {
                if (line.ProductId == product)
                {
                    line.Quantity = quantity;
                }
                if (line.Quantity == 0)
                {
                    currentCart.CartList.Remove(line);
                }
            }
        }
        public int CheckInt()
        {
            int selection = 0;
            bool selected = int.TryParse(Console.ReadLine(), out selection);
            if (selected)
            {
                return selection;
            }
            else
            {
                return -1;
            }
        }
        public string CheckString()
        {
            string input = Console.ReadLine();
            if (input != null && input != "")
            {
                return input;
            }
            else
            {
                return "null";
            }
        }
        public void PopulateStoreProductList()
        {
            List<int> productList = new List<int>();
            foreach (InventoryLine line in DbContext.InventoryLines)
            {
                if (line.LocationId == currentCustomer.DefaultStore)
                {
                    productList.Add(line.ProductId);
                }
            }
            foreach (int prodInt in productList)
            {
                foreach(Product prodAdd in DbContext.Products)
                {
                    storeProductList.Add(prodAdd);
                }
            }
        }
        public void ValidateLocationTable()
        {
            if (DbContext.Locations.Count() == 0)
            {
                DbContext.Locations.Add(new Location("New York, NY"));
                DbContext.Locations.Add(new Location("Orlando, FL"));
                DbContext.Locations.Add(new Location("Los Angeles, CA"));
                DbContext.Locations.Add(new Location("Palo Alto, CA"));
                DbContext.Locations.Add(new Location("Chicago, IL"));
            }
            DbContext.SaveChanges();
            foreach (Location store in DbContext.Locations)
            {
                storeList.Add(store);
            }
        }
        public void ValidateProductTable()
        {
            if (DbContext.Products.Count() == 0)
            {
                List<Product> productList = new List<Product>();
                string[] descriptions = {"Ball-in-a-Cup Game", "Left Shoe", "Broken Soldering Iron", "Wedge of Cheese", "Straightened Paperclip"};
                decimal[] prices = {5.00m, 7.83m, 8.41m, 4.99m, 0.99m};
                for (int i = 0; i < 5; i++)
                {
                    Product newProduct = new Product();
                    newProduct.Description = $"{descriptions[i]}";
                    newProduct.Price = prices[i];
                    DbContext.Products.Add(newProduct);                  
                }
                DbContext.SaveChanges();
            }

        }
        public void ValidateInventory()
        {
            if (DbContext.InventoryLines.Count() == 0)
            {
                foreach (Location location in DbContext.Locations)
                {
                    foreach (Product product in DbContext.Products)
                    {
                        InventoryLine line = new InventoryLine();
                        line.LocationId = location.LocationId;
                        line.ProductId = product.ProductId;
                        line.Quantity = 99;
                        DbContext.InventoryLines.Add(line);
                    }
                }
                DbContext.SaveChanges();
            }
        }
    }
}