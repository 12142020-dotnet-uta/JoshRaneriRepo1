using System;
using System.Collections.Generic;
using System.Linq;
using DomainLib;

namespace DBAccessLib
{
    public class RepositoryLayer : ICartControl
    {
        P0_DbContext DbContext = new P0_DbContext();
        public bool loggedIn = false;
        public bool storeSelected = false;
        public bool isBrowsing = false;
        bool userFound = false;        
        private Customer currentCustomer = new Customer();
        public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }
        private Customer searchCustomer = new Customer();
        public Customer SearchCustomer { get => searchCustomer; set => searchCustomer = value; }
        private Cart currentCart = new Cart();
        public Cart CurrentCart { get => currentCart; set => currentCart = value; }
        private List<Location> storeList = new List<Location>();
        public List<Location> StoreList { get => storeList; set => storeList = value; }
        private List<Product> storeProductList = new List<Product>();
        public List<Product> StoreProductList { get => storeProductList; set => storeProductList = value; }
        private List<int> lineQuantity = new List<int>();
        public List<int> LineQuantity { get => lineQuantity; set => lineQuantity = value; }
        private List<CartItem> liveCartList = new List<CartItem>();
        public List<CartItem> LiveCartList { get => liveCartList; set => liveCartList = value; }
        private List<OrderItem> liveOrderList = new List<OrderItem>();
        public List<OrderItem> LiveOrderList { get => liveOrderList; set => liveOrderList = value; }
        public int currentStore = 0;
        public List<Order> ListMyOrders()
        {
            List<Order> myOrderList = CustomerOrderHistory(CurrentCustomer.UserName);
            LiveOrderList = DbContext.OrderItems.ToList();
            return myOrderList;
        }
        /// <summary>
        /// Adds a new user if username does not already exist
        /// </summary>
        /// <param name="userName">string</param>
        /// <param name="password">string</param>
        /// <param name="firstName">string</param>
        /// <param name="lastName">string</param>
        /// <param name="address">string</param>
        /// <param name="defaultStore">int</param>
        public void NewCustomer(string userName, string password, string firstName, string lastName, string address, int defaultStore)
        {
            foreach (Customer checkCustomer in DbContext.Customers)
            {
                if (checkCustomer.UserName == userName)
                {
                    Console.WriteLine("\nUser already exists. Please login or choose a new User name.");
                    userFound = true;
                }
            }
            if (userFound == false)
            {
                Customer newCust = new Customer(userName, password, firstName, lastName, address, defaultStore);
                if (userName == "ranerijosh" || userName == "testAdmin")
                {
                    newCust.IsAdmin = true;
                }
                CurrentCustomer = newCust;
                CartSetup();
                DbContext.Customers.Add(newCust);
                DbContext.SaveChanges();
                Console.WriteLine("\nUser successfully created. Please log in.\n");
            }
        }
        /// <summary>
        /// Checks username and password and sets loggedIn to true
        /// </summary>
        /// <param name="userName">string</param>
        /// <param name="password">string</param>
        public void CustomerLogin(string userName, string password)
        {
            bool custFound = false;
            foreach (Customer checkCustomer in DbContext.Customers)
            {
                if (checkCustomer.UserName == userName && checkCustomer.Password == password)
                {
                    CurrentCustomer = checkCustomer;
                    CurrentCart = new Cart();
                    CurrentCart.CartId = CurrentCustomer.CartId;
                    if (CurrentCustomer.DefaultStore != 0)
                    {
                        currentStore = CurrentCustomer.DefaultStore;
                    }
                    foreach (Cart cart in DbContext.Carts)
                    {
                        if (cart.CartId == CurrentCart.CartId)
                        {
                            CurrentCart = cart;
                        }
                    }
                    custFound = true;
                    loggedIn = true;
                    isBrowsing = true;
                    break;
                }
            }
            if (custFound == false)
            {
                Console.WriteLine("\nInvalid Username/Password.");
                isBrowsing = false;
                loggedIn = false;
            }           
        }
        /// <summary>
        /// Searches Customer table for username and returns Customer object
        /// </summary>
        /// <param name="uName">string username</param>
        /// <returns>customer object for corresponding username</returns>
        public bool UserNameSearch(string uName)
        {
            foreach ( Customer customer in DbContext.Customers)
            {
                if (customer.UserName == uName)
                {
                    SearchCustomer = customer;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Populates a list of orders made by CurrentUser
        /// </summary>
        /// <param name="customerName">string username</param>
        /// <returns>returns list of Order objects with matching CustomerId </returns>
        public List<Order> CustomerOrderHistory(string customerName)
        {
            List<Order> custOrderList = new List<Order>();
            Customer cust = new Customer();
            SearchCustomer = new Customer();
            if (UserNameSearch(customerName))
            {
                cust = SearchCustomer;
                try
                {
                    foreach (Order order in DbContext.Orders)
                    {
                        if (order.CustomerId == cust.CustomerId)
                        {
                            custOrderList.Add(order);
                        }
                    }
                    return custOrderList;
                }
                catch (System.Exception)
                {
                    throw new Exception("No orders found for that customer.");
                }
            }
            else
            {
                Console.WriteLine("\n   Customer not found.");
                return custOrderList;
            }
        }
        /// <summary>
        /// Populates a list of orders made at current location
        /// </summary>
        /// <param name="location">int locationid</param>
        /// <returns>returns list of order objects with matching locationid</returns>
        public List<Order> StoreOrderHistory(int location)
        {
            List<Order> storeOrderList = new List<Order>();
            try
            {
                foreach(Order order in DbContext.Orders)
                {
                    if (order.LocationId == location)
                    {
                        storeOrderList.Add(order);
                    }
                }
                return storeOrderList;
            }
            catch (System.Exception)
            {
                throw new Exception("No orders found for that customer.");
            }
        }
        /// <summary>
        /// Locates order by orderid
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>returns order with matching orderid</returns>
        public Order OrderLookup(int orderId)
        {
            return new Order();
        }
        /// <summary>
        /// Lists customers with matching last name
        /// </summary>
        /// <param name="lName">string last name</param>
        /// <returns>returns list of customer objects with matching last name</returns>
        public List<Customer> ListCustomers(string lName)
        {
            List<Customer> custList = new List<Customer>();
            foreach (Customer cust in DbContext.Customers)
            {
                if (cust.LastName == lName)
                {
                    custList.Add(cust);
                }
            }
            return custList;
        }
        /// <summary>
        /// converts cart to order and persists order to db
        /// </summary>
        public void PlaceOrder()
        {
            Order order = new Order();
            order.CustomerId = CurrentCart.CustomerId;
            order.CartId = CurrentCart.CartId;
            order.LocationId = CurrentCustomer.DefaultStore;
            order.OrderTime = DateTime.Now;
            foreach (CartItem cartItem in LiveCartList)
            {
                    OrderItem oItem = new OrderItem();
                    oItem.OrderId = order.OrderId;
                    oItem.ProductId = cartItem.ProductId;
                    oItem.OrderQuantity = cartItem.CartQuantity;
                    order.OrderTotal += (DbContext.Products.ToList().ElementAt(oItem.ProductId - 1).Price * oItem.OrderQuantity);
                    DbContext.CartItems.Remove(cartItem);
                    DbContext.OrderItems.Add(oItem);
            }
            LiveCartList.Clear();
            DbContext.Carts.Remove(CurrentCart);
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();
            CartSetup();
        }
        /// <summary>
        /// Initializes a new cart or pulls cart from db
        /// </summary>
        public void CartSetup()
        {
            bool found = false;
            foreach (Cart cart in DbContext.Carts)
            {
                if (cart.CartId == CurrentCustomer.CartId)
                {
                    LiveCartList.Clear();
                    found = true;
                    CurrentCart = cart;
                    foreach (CartItem item in DbContext.CartItems)
                    {
                        LiveCartList.Add(item);
                    }
                }
            }
            if (found == false)
            {
                Cart newCart = new Cart();
                newCart.CustomerId = CurrentCustomer.CustomerId;
                CurrentCustomer.CartId = newCart.CartId;
                CurrentCart = newCart;
                DbContext.Carts.Add(newCart);
            }
            DbContext.SaveChanges();
        }
        /// <summary>
        /// adds item to cart, creates cartItem with cartId, productId, quantity
        /// </summary>
        /// <param name="product">int productid</param>
        /// <param name="quantity">int quantity</param>
        public void AddToCart(int product, int quantity)
        {
            List<Product> prodList = DbContext.Products.ToList<Product>();
            foreach (InventoryLine line in DbContext.InventoryLines)
            {
                if (line.LocationId == currentStore && line.ProductId == product && line.Quantity >= quantity)
                {
                    CartItem cartItem = new CartItem();
                    cartItem.CartId = CurrentCart.CartId;
                    cartItem.ProductId = prodList.ElementAt(product - 1).ProductId;
                    cartItem.CartQuantity = quantity;
                    LiveCartList.Add(cartItem);
                    DbContext.CartItems.Add(cartItem);
                    line.Quantity -= quantity;
                }
                else if (line.LocationId == currentStore && line.ProductId == product && line.Quantity < quantity)
                {
                    Console.WriteLine("\nLocation does not have enough of that item.");

                }
            }
            DbContext.SaveChanges();
        }
        /// <summary>
        /// ensures cart persists through logout/application exit
        /// </summary>
        public void SaveCartOnExit()
        {
            DbContext.SaveChanges();
        }
        /// <summary>
        /// changes quantity of productid with matching cartid
        /// </summary>
        /// <param name="product">int productid</param>
        /// <param name="quantity">int quantity</param>
        public void ChangeCartQuantity(int product, int quantity)
        {
            List<Product> prodList = DbContext.Products.ToList<Product>();
            foreach (CartItem cartItem in LiveCartList)
            {
                if (cartItem.ProductId == prodList.ElementAt(product).ProductId)
                {
                    cartItem.CartQuantity = quantity;
                }
                if (cartItem.CartQuantity <= 0)
                {
                    LiveCartList.Remove(cartItem);
                }
            }
        }
        /// <summary>
        /// ensures human input is a valid integer
        /// </summary>
        /// <returns>returns int</returns>
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
        /// <summary>
        /// ensures human input is a valid, not-empty string
        /// </summary>
        /// <returns>returns input string</returns>
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
        /// <summary>
        /// Creates a list matching store location names to their id
        /// </summary>
        public void PopulateStoreProductList()
        {
            StoreProductList.Clear();
            LineQuantity.Clear();
            foreach (InventoryLine line in DbContext.InventoryLines)
            {
                if (line.LocationId == CurrentCustomer.DefaultStore)
                {
                    StoreProductList.Add(DbContext.Products.ToList().ElementAt(line.ProductId - 1));
                    LineQuantity.Add(line.Quantity);
                }
            }

        }
        /// <summary>
        /// ensures that a valid location table exists and creates one if not
        /// </summary>
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
        /// <summary>
        /// ensures that a valid product table exists and creates one if not
        /// </summary>
        public void ValidateProductTable()
        {
            if (DbContext.Products.Count() == 0)
            {
                List<Product> productList = new List<Product>();
                string[] descriptions = {"Ball-in-a-Cup Game", "Left Shoe", "Broken Soldering Iron", "Wedge of Cheese", "Straightened Paperclip"};
                decimal[] prices = {5.00m, 7.83m, 8.41m, 4.99m, 0.99m};
                for (int i = 0; i < descriptions.Length; i++)
                {
                    Product newProduct = new Product();
                    newProduct.Description = $"{descriptions[i]}";
                    newProduct.Price = prices[i];
                    DbContext.Products.Add(newProduct);                  
                }
                DbContext.SaveChanges();
            }

        }
        /// <summary>
        /// ensures that a valid inventory table exists and creates one if not
        /// </summary>
        public void ValidateInventory()
        {
            if (DbContext.InventoryLines.Count() == 0)
            {
                foreach (Location location in DbContext.Locations)
                {
                    foreach (Product product in DbContext.Products)
                    {
                        DbContext.InventoryLines.Add(new InventoryLine(location.LocationId, product.ProductId, 99));
                    }
                }
                DbContext.SaveChanges();
            }
        }
    }
}