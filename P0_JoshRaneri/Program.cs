using System;
using System.Collections.Generic;
using System.Linq;
using DBAccessLib;
using DomainLib;

namespace P0_JoshRaneri
{
    class Program
    {
        static RepositoryLayer appContext = new RepositoryLayer();
        static void Main(string[] args)
        {
            appContext.ValidateProductTable();
            appContext.ValidateLocationTable(); 
            appContext.ValidateInventory();
            Console.WriteLine("\n            Welcome to Josh's Bizarre Bazaar");
            DrawLoginMenu();

            do 
            {
                appContext.PopulateStoreProductList();
                appContext.CartSetup();
                DrawNavMenu();
            } while(appContext.isBrowsing == true);
        }
        /// <summary>
        /// Draws Login Menu and processes user input
        /// </summary>
        static void DrawLoginMenu()
        {
            do
            {
                Console.WriteLine("\n   Please log in or create a new account.\n");
                Console.WriteLine("      1) Login");
                Console.WriteLine("      2) New User");
                Console.WriteLine("      3) Quit Program\n");
                Console.Write("  >: ");

                int choice = appContext.CheckInt();
                string inUser = "";
                string inPass = "";
                string inFirst = "";
                string inLast = "";
                string inAddress = "";

                switch (choice)
                {
                    case 1:
                        {
                            do
                            {
                                Console.Write("\nPlease enter your user name: ");
                                inUser = appContext.CheckString();
                            } while (inUser == "null");
                            do
                            {
                                Console.Write("\nPlease enter your password: ");
                                inPass = appContext.CheckString();
                            } while (inPass == "null");
                            appContext.CustomerLogin(inUser, inPass);
                            break;
                        }
                    case 2:
                        {
                            do
                            {
                                Console.Write("\nPlease Enter a new user name: ");
                                inUser = appContext.CheckString();
                                if (appContext.UserNameSearch(inUser))
                                {
                                    Console.WriteLine("\nThat username is already in use.\n");
                                    inUser = "null";
                                }
                            } while (inUser == "null");
                            do
                            {
                                Console.Write("\nPlease Enter a new password: ");
                                inPass = appContext.CheckString();
                            } while (inPass == "null");
                            do
                            {
                                Console.Write("\nPlease Enter your first name: ");
                                inFirst = appContext.CheckString();
                            } while (inFirst == "null");
                            do
                            {
                                Console.Write("\nPlease Enter your last name: ");
                                inLast = appContext.CheckString();
                            } while (inLast == "null");
                            do
                            {
                                Console.Write("\nPlease Enter your address: ");
                                inAddress = appContext.CheckString();
                            } while (inAddress == "null");
                            do
                            {
                                Console.WriteLine("\nPlease select your default store location.\n");
                                appContext.currentStore = DrawLocationMenu();
                            } while (appContext.storeSelected != true);
                            appContext.NewCustomer(inUser, inPass, inFirst, inLast, inAddress, appContext.currentStore);
                            break;
                        }
                    case 3:
                        {
                            appContext.SaveCartOnExit();
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nPlease enter valid input.\n");
                            break;
                        }
                }
            } while (appContext.loggedIn != true);
        }
        /// <summary>
        /// Draws location selection menu
        /// </summary>
        /// <returns>returns int locationid</returns>
        static int DrawLocationMenu()
        {
            foreach (Location store in appContext.StoreList)
            {
                Console.WriteLine($"   {store.LocationId}) {store.LocationName}");
            }
            Console.Write("\n  >: ");
            
            int choice = appContext.CheckInt();
            if (choice <= 5 && choice > 0){
                appContext.storeSelected = true;
            }
            else
            {
                Console.WriteLine("\nPlease enter a valid choice.\n");
                DrawLocationMenu();
            }
            return choice;
        }
        /// <summary>
        /// Draws Navigation Menu based on admin priveleges
        /// </summary>
        static void DrawNavMenu()
        {
            int choice = 0;
            do
            {
            Customer dispCustomer = appContext.CurrentCustomer;
            if (!dispCustomer.IsAdmin)
            {
                string userStore = "";
                foreach (Location loc in appContext.StoreList)
                {
                    if (loc.LocationId == dispCustomer.DefaultStore)
                    {
                        userStore = loc.LocationName;
                    }
                }
                Console.WriteLine($"\n   Current Location: {userStore}  -  Current Customer: {dispCustomer.UserName}  -  {dispCustomer.FirstName} {dispCustomer.LastName}\n");
                Console.WriteLine($"      1) Shop {userStore}");
                Console.WriteLine("      2) View Current Cart");
                Console.WriteLine("      3) View Order History");
                Console.WriteLine("      4) View Order Details");
                Console.WriteLine("      5) Change Default Location");
                Console.WriteLine("      6) Logout");
                Console.Write("\n  >: ");
                choice = appContext.CheckInt();
                switch (choice)
                {
                    case 1:
                        {
                            DrawShopMenu();
                            break;
                        }
                    case 2:
                        {
                            PrintCustomerCart(dispCustomer.UserName);
                            break;
                        }
                    case 3:
                        {
                            PrintCustomerHistory(dispCustomer.UserName);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("\n   Please select an order to view\n");
                            
                            List<Order> myOrderList = appContext.ListMyOrders();
                            foreach (Order order in myOrderList)
                            {
                                Console.WriteLine($"      {(myOrderList.IndexOf(order) + 1)}) {(appContext.StoreList.ElementAt(order.LocationId - 1).LocationName)} - {order.OrderTime}");
                            }
                            Console.Write("\n  >: ");
                            int orderNum = appContext.CheckInt();
                            DrawOrderDetails(myOrderList.ElementAt(orderNum - 1));
                            break;
                        }
                    case 5:
                        {
                            appContext.CurrentCustomer.DefaultStore = DrawLocationMenu();
                            break;
                        }
                    case 6:
                        {
                            appContext.loggedIn = false;
                            appContext.isBrowsing = false;
                            appContext.CurrentCustomer = null;
                            appContext.CurrentCart = null;
                            DrawLoginMenu();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter a valid selection.");
                            DrawNavMenu();
                            break;
                        }
                }
            }
            else if (dispCustomer.IsAdmin)
            {
                string userStore = "";
                foreach (Location loc in appContext.StoreList)
                {
                    if (loc.LocationId == dispCustomer.DefaultStore)
                    {
                        userStore = loc.LocationName;
                    }
                }
                Console.WriteLine($"\n   Current Location: {userStore}  -  Current Customer: {dispCustomer.UserName} (Admin)  -  {dispCustomer.FirstName} {dispCustomer.LastName}\n");
                Console.WriteLine($"      1) Shop {userStore}");
                Console.WriteLine("      2) View Current Cart");
                Console.WriteLine("      3) View Order History");
                Console.WriteLine("      4) Change Default Location");
                Console.WriteLine("      5) View My Order Details");
                Console.WriteLine("      6) View Other Records");
                Console.WriteLine("      7) Logout");
                Console.Write("\n  >: ");
                choice = appContext.CheckInt();
                switch (choice)
                {
                    case 1:
                        {
                            DrawShopMenu();
                            break;
                        }
                    case 2:
                        {
                            PrintCustomerCart(dispCustomer.UserName);
                            break;
                        }
                    case 3:
                        {
                            PrintCustomerHistory(dispCustomer.UserName);
                            break;
                        }
                    case 4:
                        {
                            appContext.CurrentCustomer.DefaultStore = DrawLocationMenu();
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("\n   Please select an order to view\n");
                            
                            List<Order> myOrderList = appContext.ListMyOrders();
                            foreach (Order order in myOrderList)
                            {
                                Console.WriteLine($"      {(myOrderList.IndexOf(order) + 1)}) {(appContext.StoreList.ElementAt(order.LocationId - 1).LocationName)} - {order.OrderTime}");
                            }
                            Console.Write("\n  >: ");
                            int orderNum = appContext.CheckInt();
                            DrawOrderDetails(myOrderList.ElementAt(orderNum - 1));
                            break;
                        }
                    case 6:
                        {
                            DrawRecordSearchMenu();
                            break;
                        }
                    case 7:
                        {
                            appContext.loggedIn = false;
                            appContext.CurrentCustomer = null;
                            appContext.CurrentCart = null;
                            appContext.isBrowsing = false;
                            DrawLoginMenu();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter a valid selection.");
                            DrawNavMenu();
                            break;
                        }
                }
            }
            } while (choice != 6);
        }
        /// <summary>
        /// Draws Record Search Menu and renders resulting listings
        /// </summary>

        static void DrawRecordSearchMenu()
        {
            int choice = 0;
            do
            {
            Console.WriteLine("\n   Record Search\n");
            Console.WriteLine("      1) Search for customer by Username");
            Console.WriteLine("      2) List Usernames by Last Name");
            Console.WriteLine("      3) Display Customer Order History");
            Console.WriteLine("      4) Display Location Order History");
            Console.WriteLine("      5) Return to Previous Menu\n");
            Console.Write("  >: ");
            choice = appContext.CheckInt();

            switch (choice)
            {
                case 1:
                    {
                        Console.Write("\n Please enter Username to find: ");
                        string user = appContext.CheckString();
                        Customer dispCustomer = new Customer();
                        if (appContext.UserNameSearch(user))
                        {
                            dispCustomer = appContext.SearchCustomer;
                        }
                        else 
                        {
                            Console.WriteLine("\n   Customer with that username not found.");
                            DrawRecordSearchMenu();
                        }
                        string userStore = "";
                        foreach (Location loc in appContext.StoreList)
                        {
                            if (loc.LocationId == dispCustomer.DefaultStore)
                            {
                                userStore = loc.LocationName;
                            }
                        }
                        Console.WriteLine("\n   Customer Details");
                        Console.WriteLine($"\n      Username: {dispCustomer.UserName}");
                        Console.WriteLine($"      Name: {dispCustomer.FirstName} {dispCustomer.LastName}");
                        Console.WriteLine($"      Address: {dispCustomer.Address}");
                        Console.WriteLine($"      Default Store: {userStore}");
                        break;
                    }
                case 2:
                    {
                        Console.Write("\nPlease enter the Last Name to find: ");
                        string lName = appContext.CheckString();
                        List<Customer> custList = appContext.ListCustomers(lName);
                        foreach (Customer cust in custList)
                        {
                            Console.WriteLine("\n   Customer Details");
                            Console.WriteLine($"\n      Username: {cust.UserName}");
                            Console.WriteLine($"      Name: {cust.FirstName} {cust.LastName}");  
                            Console.WriteLine($"      Address: {cust.Address}");                         
                        }                        
                        break;
                    }
                case 3:
                    {
                        Console.Write("\n Enter Username to list Orders for: ");
                        string user = appContext.CheckString();
                        PrintCustomerHistory(user);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("\n   Location Order History\n");
                        int storeNumber = DrawLocationMenu();
                        List<Order> orderList = appContext.StoreOrderHistory(storeNumber);
                        string userStore = "";
                        foreach (Location loc in appContext.StoreList)
                        {
                            if (loc.LocationId == storeNumber)
                            {
                                userStore = loc.LocationName;
                            }
                        }
                        Console.WriteLine($"\n      Location: {userStore}");
                        foreach (Order orderLine in orderList)
                        {
                            Console.WriteLine($"\n      Order Id: {orderLine.OrderId}");
                            Console.WriteLine($"      Location: {userStore}");
                            Console.WriteLine($"      Order Date: {orderLine.OrderTime}");
                            Console.WriteLine($"      Order Total: ${orderLine.OrderTotal}\n");
                        }
                        break;
                    }
                case 5:
                    {
                        DrawNavMenu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\nPlease enter a valid choice.\n");
                        DrawRecordSearchMenu();
                        break;
                    }
            }
            }while (choice != 5);
        }
        /// <summary>
        /// Draws shop menu and allows user to select products to add to cart
        /// </summary>
        static void DrawShopMenu()
        {
            appContext.PopulateStoreProductList();
            int count = 0;
            Console.WriteLine("\n   Please choose from our current inventory.\n");
            foreach (Product product in appContext.StoreProductList)
            {
                Console.WriteLine($"   {product.ProductId}) {product.Description} ");
                Console.WriteLine($"       ${Math.Round(product.Price, 2)}    Quantity On Hand: {appContext.LineQuantity.ElementAt(product.ProductId - 1)}");
                count++;
            }
            Console.WriteLine($"   {(count + 1)}) View Cart");
            Console.WriteLine($"   {(count + 2)}) Place order");
            Console.WriteLine($"   {(count + 3)}) Return to previous menu\n");
            Console.Write("  >: ");
                
            int choice = appContext.CheckInt();
            if (choice > 0 && choice <= 5)
            {
                Console.Write("   Please enter desired quantity: ");
                int quant = appContext.CheckInt();
                appContext.AddToCart(choice, quant);
                appContext.PopulateStoreProductList();
                DrawShopMenu();
            }
            else if (choice == (count + 1))
            {
                int prodCount = 1;
                decimal total = 0;
                Console.WriteLine("\n      Your Cart\n");
                foreach (CartItem cartItem in appContext.LiveCartList)
                {
                    decimal item = Math.Round((appContext.StoreProductList.ElementAt(cartItem.ProductId - 1).Price * cartItem.CartQuantity), 2);
                    Console.WriteLine($"   {prodCount}) {appContext.StoreProductList.ElementAt(cartItem.ProductId - 1).Description}    x    {cartItem.CartQuantity}    -    ${item}");
                    total += item;
                    prodCount++;
                }
                Console.WriteLine($"      Cart Total: ${total}");
                Console.WriteLine($"\n   {prodCount + 1}) Return");
                int cartChoice = appContext.CheckInt();
                if (cartChoice == (prodCount + 1))
                {
                    DrawShopMenu();
                }
            }
            else if (choice == (count + 2))
            {
                appContext.PlaceOrder();
            }
            else if (choice == (count + 3))
            {
                DrawNavMenu();
            }
            else 
            {
                DrawShopMenu();
            }
        }
        static void DrawOrderDetails(Order order)
        {
            Console.WriteLine($"\n   Order Details - {(appContext.StoreList.ElementAt(order.LocationId - 1).LocationName)} - {order.OrderTime}\n");
            foreach (OrderItem item in appContext.LiveOrderList)
            {
                if (item.OrderId == order.OrderId)
                {
                    Console.WriteLine($"      {(appContext.StoreProductList.ElementAt(item.ProductId - 1).Description)}   x   {item.OrderQuantity}   =   ${(item.OrderQuantity * appContext.StoreProductList.ElementAt(item.ProductId - 1).Price)}");
                }
            }
            Console.WriteLine($"\n   Order Total: ${order.OrderTotal}\n");
            Console.ReadLine();
        }
        /// <summary>
        /// outputs customer order history with matching username
        /// </summary>
        /// <param name="user">string username</param>
        static void PrintCustomerHistory(string user)
        {
            Customer dispCustomer = new Customer();
            if (appContext.UserNameSearch(user))
            {
                dispCustomer = appContext.SearchCustomer;
            }
            else 
            {
                Console.WriteLine("\n   Customer with that username not found.\n");
                DrawRecordSearchMenu();
            }
            List<Order> orderList = appContext.CustomerOrderHistory(user);
            string userStore = "";
            foreach (Location loc in appContext.StoreList)
            {
                if (loc.LocationId == dispCustomer.DefaultStore)
                {
                    userStore = loc.LocationName;
                }
            }
            Console.WriteLine($"\n      Customer: {dispCustomer.UserName} - {dispCustomer.FirstName} {dispCustomer.LastName}");
            Console.WriteLine($"      Address: {dispCustomer.Address}");
            foreach (Order orderLine in orderList)
            {
                Console.WriteLine($"\n      Order Id: {orderLine.OrderId}");
                Console.WriteLine($"      Location: {userStore}");
                Console.WriteLine($"      Order Date: {orderLine.OrderTime}");
                Console.WriteLine($"      Order Total: ${orderLine.OrderTotal}\n");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// outputs current customer's current active cart contents
        /// </summary>
        /// <param name="user">string username</param>
        static void PrintCustomerCart(string user)
        {
            Customer dispCustomer = new Customer();
            if (appContext.UserNameSearch(user))
            {
                dispCustomer = appContext.SearchCustomer;
            }
            else 
            {
                Console.WriteLine("\n   Customer with that username not found.\n");
                DrawNavMenu();
            }
            appContext.CartSetup();
            List<string> printList = new List<string>();
            Console.WriteLine($"\n      Customer: {dispCustomer.UserName} - {dispCustomer.FirstName} {dispCustomer.LastName}\n");
            int prodCount = 1;
            decimal total = 0;
            foreach (CartItem cartItem in appContext.LiveCartList)
            {
                decimal item = Math.Round((appContext.StoreProductList.ElementAt(cartItem.ProductId - 1).Price * cartItem.CartQuantity), 2);
                Console.WriteLine($"   {prodCount}) {appContext.StoreProductList.ElementAt(cartItem.ProductId - 1).Description}    x    {cartItem.CartQuantity}    -    ${item}");
                total += item;
                prodCount++;
            }
            Console.WriteLine($"\n      Cart Total: ${total}\n");
            Console.ReadLine();
        }
    }
}
