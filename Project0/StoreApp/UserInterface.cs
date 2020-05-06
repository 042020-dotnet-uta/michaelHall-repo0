using Microsoft.EntityFrameworkCore.Storage;
using StoreApp.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreApp.BusinessLogic;
using System.Security.Cryptography.X509Certificates;

namespace StoreApp
{
    interface IUserInterface
    {
        void StartApp();
        void MainMenu();
        bool IsValidMenuInput(string input);
        void AddNewCustomer();
        void AddNewOrder();
        void CustomerSearch();
        void DisplayOrderDetails();
        void DisplayStoreHistory();
        void DisplayCustomerHistory();
    }

    class UserInterface : IUserInterface
    {
        #region Fields & Properties

        #endregion

        #region Methods
        public void StartApp()
        {
            //using (StoreApp_DbContext db = new StoreApp_DbContext())
            //{
            //Database_Initializer InitializeDb = new Database_Initializer();
            //InitializeDb.SetUpDatabase();
            Console.WriteLine("Welcome to the Store!");
                
                string input = "";
                do
                {
                    // prints out the instruction menu
                    MainMenu();
                    do
                    {
                        // gets input from the user
                        input = Console.ReadLine();
                    } while (!IsValidMenuInput(input)); // checks if input is valid
                } while (input != "0"); // if fourth option, exit the program
           //}
        }

        public void MainMenu()
        {
            Console.WriteLine("Do one of the following by entering in the respective number:");
            Console.WriteLine("\t 1: Add a new customer");
            Console.WriteLine("\t 2: Place a new order for a Customer");
            Console.WriteLine("\t 3: Search for a customer by name");
            Console.WriteLine("\t 4: look at a specific order's details");
            Console.WriteLine("\t 5: look at a specific store's order history");
            Console.WriteLine("\t 6: Look at a specific customer's order history");
            Console.WriteLine("\t 0: Exit the application");
            Console.Write("Select: ");
        }

        public bool IsValidMenuInput(string input)
        {
            // switch case to determine which option
            switch (input)
            {
                case "1": // Add new customer
                    AddNewCustomer();
                    break;
                case "2": // Place new order
                    AddNewOrder();
                    break;
                case "3": // Search for customer(s)
                    CustomerSearch();
                    break;
                case "4":
                    DisplayOrderDetails();
                    break;
                case "5":
                    DisplayStoreHistory();
                    break;
                case "6":
                    DisplayCustomerHistory();
                    break;
                case "0": // exits
                    break;
                default: // try again if option was not picked
                    Console.WriteLine("Invalid option, please enter on of the options above.");
                    return false;
            }
            return true;
        }

        public void AddNewCustomer()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                CustomerCreation createCustomer = new CustomerCreation();
                Customer newCustomer = new Customer();

                Console.WriteLine("What's the first name of the Customer?");
                newCustomer.FirstName = Console.ReadLine();
                while (!createCustomer.IsValidName(newCustomer.FirstName)) 
                {
                    Console.WriteLine("Invalid first name, please enter another .");
                    newCustomer.FirstName = Console.ReadLine(); 
                }

                Console.WriteLine("What's the last name of the customer?");
                newCustomer.LastName = Console.ReadLine();
                while (!createCustomer.IsValidName(newCustomer.LastName)) 
                {
                    Console.WriteLine("Invalid last name, please enter another");
                    newCustomer.LastName = Console.ReadLine();
                }

                Console.WriteLine("What would you like the username for the customer to be?");
                newCustomer.UserName = Console.ReadLine();
                while (!createCustomer.IsValidUserName(newCustomer.UserName)) 
                {
                    Console.WriteLine("Invalid username, has to be 8 to 20 characters.");
                    newCustomer.UserName = Console.ReadLine(); 
                }
                db.Add<Customer>(newCustomer);
                db.SaveChanges();
                Console.WriteLine("Customer successfully added! Hit enter to go back to menu.");
                Console.ReadLine();
            }
        }
        
        public void AddNewOrder()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                OrderCreation createOrder = new OrderCreation();
                CustomerQueries checkCustomer = new CustomerQueries();
                Order newOrder = new Order();

                Console.WriteLine("Please enter the customerID of your Customer placing an order.");
                do
                {
                    string input = Console.ReadLine();
                    while (!createOrder.IsValidNum(input))
                    {
                        Console.WriteLine("Invalid customerID number, please enter another.");
                        input = Console.ReadLine();
                    }
                    int id = createOrder.StringToInt(input);
                    if (checkCustomer.IsValidCustomerID(id))
                    {
                        newOrder.CustomerID = id;
                    }
                    else
                    {
                        Console.WriteLine("There is no Customer with this ID, please enter another.");
                        newOrder.CustomerID = 0;
                    }
                } while (newOrder.CustomerID == 0);

                ProductQueries checkProducts = new ProductQueries();
                var products = checkProducts.GetProducts();
                Console.WriteLine("Here are all the available products:");
                Console.WriteLine("ID\tStore ID\tName\t\tInventory\tPrice");
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductID}\t{p.StoreID}\t\t{p.ProductName}" +
                        $"\t{p.Inventory}\t\t{p.Price}");
                }

                bool multipleProducts;
                int productCount = 0;

                do
                {
                    Console.WriteLine("Please enter the ID of the product being ordered");

                    do
                    {
                        string input = Console.ReadLine();
                        while (!createOrder.IsValidNum(input))
                        {
                            Console.WriteLine("Invalid product ID number, please enter another.");
                            input = Console.ReadLine();
                        }
                        int id = createOrder.StringToInt(input);
                        if (checkProducts.IsValidProductID(id))
                        {
                            newOrder.ProductID = id;
                        }
                        else
                        {
                            Console.WriteLine("There is no product with this ID or there is none left, please enter another.");
                            newOrder.ProductID = 0;
                        }
                    } while (newOrder.ProductID == 0);

                    var product = checkProducts.GetProductName(newOrder.ProductID);
                    Console.WriteLine($"For buying, specify the number of {product.ProductName}");

                    do
                    {
                        string input = Console.ReadLine();
                        while (!createOrder.IsValidNum(input))
                        {
                            Console.WriteLine("Invalid amount, please enter another.");
                            input = Console.ReadLine();
                        }
                        int amount = createOrder.StringToInt(input);
                        if (amount == 0)
                        {
                            Console.WriteLine("Please specify an amount");
                        }
                        else if (checkProducts.IsValidProductQuantity(amount, newOrder.ProductID))
                        {
                            newOrder.Quantity = amount;
                        }
                        else if (createOrder.IsUnreasonableQuantity(amount))
                        {

                            Console.WriteLine($"{amount} is an unreasonable amount of {product.ProductName}");
                            newOrder.Quantity = 0;
                        }
                        else
                        {
                            Console.WriteLine($"There is not {amount} available at this store, please enter another amount.");
                            newOrder.Quantity = 0;
                        }
                    } while (newOrder.Quantity == 0);

                    Console.WriteLine("Would you like to include another product in this order (yes or no)?");
                    string addProduct = Console.ReadLine();
                    while (addProduct != "yes" && addProduct != "no")
                    {
                        Console.WriteLine("Please pick put in one of the two");
                        addProduct = Console.ReadLine();
                    }

                    if (addProduct == "yes")
                    {
                        multipleProducts = true;
                    }
                    else
                    {
                        multipleProducts = false;
                    }

                    productCount++;

                    if (productCount == 1)
                    {
                        newOrder.Timestamp = createOrder.GetTimeStamp();
                    }

                    db.Add<Order>(newOrder);
                    db.SaveChanges();

                    StoreQueries updateStore = new StoreQueries();
                    updateStore.UpdateInventory(newOrder);

                    newOrder.OrderID++;
                } while (multipleProducts);
                Console.WriteLine("Order successfully placed! Hit enter to go back to menu.");
                Console.ReadLine();
            }
        }

        public void CustomerSearch()
        {
            CustomerCreation validation = new CustomerCreation();
            Console.WriteLine("Please enter a customer's partial or full first name.");
            string firstName = Console.ReadLine();
            while (firstName != "" && !validation.IsValidName(firstName))
            {
                Console.WriteLine("Invalid first name, please enter another or an empty string.");
                firstName = Console.ReadLine();
            }

            Console.WriteLine("Please enter a customer's partial or full last name.");
            string lastName = Console.ReadLine();
            while (lastName != "" && !validation.IsValidName(lastName))
            {
                Console.WriteLine("Invalid last name, please enter another or an empty string.");
                lastName = Console.ReadLine();
            }

            CustomerQueries search = new CustomerQueries();
            var searchedCustomers = search.CustomerSearch(firstName, lastName);

            if (searchedCustomers.Count() == 0)
            {
                Console.WriteLine("There are no Customers matching the search parameters");
            }
            else
            {
                Console.WriteLine($"ID\tFirst Name\tLast Name\tUsername");
                foreach (var c in searchedCustomers)
                {
                    Console.WriteLine($"{c.CustomerID}\t{c.FirstName}" +
                        $"\t\t{c.LastName}\t\t{c.UserName}");
                }
                Console.Write("Search Complete! ");    
            }
            Console.WriteLine("Press enter to return to the menu");
            Console.ReadLine();
        }

        public void DisplayOrderDetails()
        {
            Console.WriteLine("Please enter the ID of the order you would like to see");
            OrderCreation createOrder = new OrderCreation(); // for Validation method
            OrderQueries checkOrder = new OrderQueries();
            int orderID;
            do
            {
                string input = Console.ReadLine();
                while (!createOrder.IsValidNum(input))
                {
                    Console.WriteLine("Invalid order ID number, please enter another.");
                    input = Console.ReadLine();
                }
                int id = createOrder.StringToInt(input);
                if (checkOrder.IsValidOrderID(id))
                {
                    orderID = id;
                }
                else
                {
                    Console.WriteLine("There is no order with this ID, please enter another.");
                    orderID = 0;
                }
            } while (orderID == 0);

            var orderDetails = checkOrder.GetOrderDetails(orderID);

            Console.WriteLine("Customer\tStore Location\t\tProduct\t\tQuantity\tTotal\tTimestamp");
            foreach (var o in orderDetails)
            {
                double price = o.Product.Price * o.Quantity;
                Console.WriteLine($"{o.Customer.FirstName} {o.Customer.LastName}\t" +
                    $"{o.Product.Store.Location}\t\t{o.Product.ProductName}\t{o.Quantity}" +
                    $"\t\t${price}\t{o.Timestamp}");
            }

            Console.WriteLine("Press enter to return to the menu");
            Console.ReadLine();
        }
        
        public void DisplayStoreHistory()
        {
            StoreQueries checkStore = new StoreQueries();
            OrderCreation checkNum = new OrderCreation();

            var stores = checkStore.GetStores();
            Console.WriteLine("ID\tLocation");
            foreach (var s in stores)
            {
                Console.WriteLine($"{s.StoreID}\t{s.Location}");
            }

            Console.WriteLine("Please enter an ID from above for the store location you would like to see.");
            int storeID;
            do
            {
                string input = Console.ReadLine();
                while (!checkNum.IsValidNum(input))
                {
                    Console.WriteLine("Invalid ID number, please enter another.");
                    input = Console.ReadLine();
                }
                int id = checkNum.StringToInt(input);
                if (checkStore.IsValidStoreID(id))
                {
                    storeID = id;
                }
                else
                {
                    Console.WriteLine("There is no store with this ID, please enter another.");
                    storeID = 0;
                }
            } while (storeID == 0);

            var storeHistory = checkStore.GetStoreHistory(storeID);
            var storeLocation = checkStore.GetStoreLocation(storeID);

            if (storeHistory.Count() == 0)
            {
                Console.WriteLine($"As of now, no orders have been made from {storeLocation}");
            }
            else
            { 
                Console.WriteLine($"Order history for {storeLocation}");
                Console.WriteLine("Customer\tProduct\t\tQuantity\tTotal\t\tTimestamp");
                foreach (var o in storeHistory)
                {
                    double price = o.Product.Price * o.Quantity;
                    Console.WriteLine($"{o.Customer.FirstName} {o.Customer.LastName}\t" +
                        $"{o.Product.ProductName}\t{o.Quantity}" +
                        $"\t\t${price}\t\t{o.Timestamp}");
                }
            }

            Console.WriteLine("Press enter to return to the menu");
            Console.ReadLine();
        }
        
        public void DisplayCustomerHistory()
        {
            CustomerQueries checkCustomer = new CustomerQueries();
            OrderCreation checkNum = new OrderCreation();

            var customers = checkCustomer.GetCustomers();
            Console.WriteLine("ID\tFirst Name\tLast Name\tUsername");
            foreach (var c in customers)
            {
                Console.WriteLine($"{c.CustomerID}\t{c.FirstName}" +
                        $"\t\t{c.LastName}\t\t{c.UserName}");
            }

            Console.WriteLine("Please enter an ID from above for the customer you would like to see.");
            int customerID;
            do
            {
                string input = Console.ReadLine();
                while (!checkNum.IsValidNum(input))
                {
                    Console.WriteLine("Invalid ID number, please enter another.");
                    input = Console.ReadLine();
                }
                int id = checkNum.StringToInt(input);
                if (checkCustomer.IsValidCustomerID(id))
                {
                    customerID = id;
                }
                else
                {
                    Console.WriteLine("There is no customer with this ID, please enter another.");
                    customerID = 0;
                }
            } while (customerID == 0);

            var customerHistory = checkCustomer.GetCustomerHistory(customerID);
            var customer = checkCustomer.GetCustomer(customerID);

            if (customerHistory.Count() == 0)
            {
                Console.WriteLine($"As of now, {customer.FirstName} {customer.LastName} has placed no orders.");
            }
            else
            {
                Console.WriteLine($"Order history for {customer.FirstName} {customer.LastName}");
                Console.WriteLine("Location\tOrder ID\tProduct\t\tQuantity\tTotal\t\tTimestamp");
                foreach (var o in customerHistory)
                {
                    double price = o.Product.Price * o.Quantity;
                    Console.WriteLine($"{o.Product.Store.Location}\t{o.OrderID}\t\t" +
                        $"{o.Product.ProductName}\t" +
                        $"{o.Quantity}\t\t${price}\t\t{o.Timestamp}");
                }
            }

            Console.WriteLine("Press enter to return to the menu");
            Console.ReadLine();
        }
        #endregion
    }
}
