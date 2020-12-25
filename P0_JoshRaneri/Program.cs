using System;
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
            appContext.ValidateInventory();
            appContext.ValidateLocationTable();
            Console.WriteLine("Welcome to Josh's Bizarre Bazaar\n");
            
            do
            {
                DrawLoginMenu();
            } while (appContext.loggedIn != true);
            do
            {
                DrawLocationMenu();
            } while(appContext.storeSelected != true);
            // do
            // {
            //     DrawShopMenu();
            // } while();
        }

        static void DrawLoginMenu()
        {
            Console.WriteLine("   Please log in or create a new account.\n");
            Console.WriteLine("      1) Login");
            Console.WriteLine("      2) New User");
            Console.WriteLine("      3) Quit Program\n");
            Console.Write("  >:");

            int choice = appContext.CheckInt();
            string inUser;
            string inPass;
            string inFirst;
            string inLast;
            string inAddress;
            int inStore;

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
                            Console.Write("\nPlease Enter a default store number: ");
                            inStore = appContext.CheckInt();
                        } while (inStore == -1);
                        appContext.NewCustomer(inUser, inPass, inFirst, inLast, inAddress, inStore);
                        break;
                    }
                case 3:
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\nPlease enter valid input.\n");
                        break;
                    }
            }
        }
        static void DrawLocationMenu()
        {
            Console.WriteLine("Please select your default store location.\n");
            foreach (Location store in appContext.StoreList)
            {
                Console.WriteLine($"   {store.LocationId}) {store.LocationName}");
            }

            Console.Write("  >:");
            
            int choice = appContext.CheckInt();
            if (choice <= 5 && choice > 0){
                appContext.CurrentCustomer.DefaultStore = choice;
                appContext.storeSelected = true;
            }
            else
            {
                Console.WriteLine("\nPlease enter a valid choice.\n");
                DrawLocationMenu();
            }
        }
        static void DrawShopMenu()
        {
            Console.WriteLine("   Please choose from our current inventory.\n");

            
            int choice = appContext.CheckInt();
        }
    }
}
