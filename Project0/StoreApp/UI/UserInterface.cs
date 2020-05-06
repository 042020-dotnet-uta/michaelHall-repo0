using Microsoft.EntityFrameworkCore.Storage;
using StoreApp.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreApp.BusinessLogic;
using System.Security.Cryptography.X509Certificates;

namespace StoreApp.UI
{
    class UserInterface : IUserInterface
    { 
        #region Methods
        /// <summary>
        /// Starts the application and guides user along
        /// </summary>
        public void StartApp()
        { 
            // Uncomment these below if the database needs to be restored.
            //Database_Initializer InitializeDb = new Database_Initializer();
            //InitializeDb.SetUpDatabase();
            Console.WriteLine("Welcome to the Bath and Shower Product Store!");
                
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
        }

        /// <summary>
        /// Prints out the menu instructions to the console.
        /// </summary>
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
            Console.WriteLine("After picking, enter in 'cancel' at any point to come back here");
            Console.Write("Select: ");
        }

        /// <summary>
        /// Switch case to check user input at the menu and determine
        /// what action to take accordingly.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                case "4": // display order details
                    DisplayOrderDetails();
                    break;
                case "5": // display store history
                    DisplayStoreHistory();
                    break;
                case "6": // display customer history
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

        /// <summary>
        /// input/output for the process of adding a new customer with all
        /// the validation taking place along the way and finally adding 
        /// a new customer with the given information.
        /// </summary>
        public void AddNewCustomer()
        {
            // create new instance
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                CustomerCreation createCustomer = new CustomerCreation();
                Customer newCustomer = new Customer();

                Console.WriteLine("What's the first name of the Customer?");
                newCustomer.FirstName = Console.ReadLine();
                if (newCustomer.FirstName == "cancel") { return; }

                while (!createCustomer.IsValidInputName(newCustomer.FirstName)) 
                {
                    Console.WriteLine("Invalid first name, please enter another .");
                    newCustomer.FirstName = Console.ReadLine();
                    if (newCustomer.FirstName == "cancel") { return; }
                }

                Console.WriteLine("What's the last name of the customer?");
                newCustomer.LastName = Console.ReadLine();
                if (newCustomer.LastName == "cancel") { return; }

                while (!createCustomer.IsValidInputName(newCustomer.LastName)) 
                {
                    Console.WriteLine("Invalid last name, please enter another");
                    newCustomer.LastName = Console.ReadLine();
                    if (newCustomer.LastName == "cancel") { return; }
                }

                Console.WriteLine("What would you like the username for the customer to be?");
                newCustomer.UserName = Console.ReadLine();
                if (newCustomer.UserName == "cancel") { return; }

                while (!createCustomer.IsValidUserName(newCustomer.UserName)) 
                {
                    Console.WriteLine("Invalid username, has to be 8 to 20 characters.");
                    newCustomer.UserName = Console.ReadLine();
                    if (newCustomer.UserName == "cancel") { return; }
                }

                db.Add<Customer>(newCustomer);
                db.SaveChanges();

                Console.WriteLine("Customer successfully added! Hit enter to go back to menu.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// input/output for the process of adding a new order with all
        /// the validation taking place along the way and finally adding 
        /// a new order with the given information.
        /// </summary>
        public void AddNewOrder()
        {
            // declare new instance(s)
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                OrderCreation createOrder = new OrderCreation();
                CustomerQueries checkCustomer = new CustomerQueries();
                Order newOrder = new Order();

                Console.WriteLine("Please enter the customerID of your Customer placing an order.");
                do
                {
                    string input = Console.ReadLine();
                    if (input == "cancel") { return; }

                    // check if input is an int
                    while (!createOrder.IsValidNum(input))
                    {
                        Console.WriteLine("Invalid customerID number, please enter another.");
                        input = Console.ReadLine();
                        if (input == "cancel") { return; }
                    }
                    
                    // check if there is a customer with the inputted ID
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
                } while (newOrder.CustomerID == 0); // repeat if there is no customer with the ID

                // display all the available products
                ProductQueries checkProducts = new ProductQueries();
                var products = checkProducts.GetProducts();
                Console.WriteLine("Here are all the available products:");
                Console.WriteLine("ID\tStore\t\tName\t\tInventory\tPrice");
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductID}\t{p.Store.Location}\t{p.ProductName}" +
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
                        if (input == "cancel") { return; }

                        // check if input is an int
                        while (!createOrder.IsValidNum(input))
                        {
                            Console.WriteLine("Invalid product ID number, please enter another.");
                            input = Console.ReadLine();
                            if (input == "cancel") { return; }
                        }

                        int id = createOrder.StringToInt(input);
                        // check if there is a product with the inputted ID
                        if (checkProducts.IsValidProductID(id))
                        {
                            newOrder.ProductID = id;
                        }
                        else
                        {
                            Console.WriteLine("There is no product with this ID or there is none left, please enter another.");
                            newOrder.ProductID = 0;
                        }
                    } while (newOrder.ProductID == 0); // repeat if no product with that ID

                    var product = checkProducts.GetProductName(newOrder.ProductID);
                    Console.WriteLine($"For buying, specify the number of {product.ProductName}");

                    do
                    {
                        string input = Console.ReadLine();
                        if (input == "cancel") { return; }

                        // check if input is an int
                        while (!createOrder.IsValidNum(input))
                        {
                            Console.WriteLine("Invalid amount, please enter another.");
                            input = Console.ReadLine();
                            if (input == "cancel") { return; }
                        }

                        int amount = createOrder.StringToInt(input);
                        // check if the inventory is high enough for given amount
                        if (amount == 0)
                        {
                            Console.WriteLine("Please specify an amount");
                        }
                        else if (createOrder.IsUnreasonableQuantity(amount))
                        {
                            // if the amount requested is unreasonable (>=10)
                            Console.WriteLine($"{amount} is an unreasonable amount of {product.ProductName}");
                            newOrder.Quantity = 0;
                        }
                        else if (checkProducts.IsValidProductQuantity(amount, newOrder.ProductID))
                        {
                            // if there is enough product and it is reasonable
                            newOrder.Quantity = amount;
                        }
                        else
                        {
                            Console.WriteLine($"There is not {amount} available at this store, please enter another amount.");
                            newOrder.Quantity = 0;
                        }
                    } while (newOrder.Quantity == 0); // repeat if not enough product or unreasonable

                    Console.WriteLine("Would you like to include another product in this order (yes or no)?");
                    string addProduct = Console.ReadLine();
                    if (addProduct == "cancel") { return; }
                    
                    // check if they are saying yes or no to extra product
                    while (addProduct != "yes" && addProduct != "no")
                    {
                        Console.WriteLine("Please pick put in one of the two");
                        addProduct = Console.ReadLine();
                        if (addProduct == "cancel") { return; }
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
                        // keep same timestamp for multiple product order
                        newOrder.Timestamp = createOrder.GetTimeStamp();
                    }

                    db.Add<Order>(newOrder);
                    db.SaveChanges();

                    StoreQueries updateStore = new StoreQueries();
                    updateStore.UpdateInventory(newOrder);

                    newOrder.OrderID++;
                } while (multipleProducts); // go back if they wanted another product
                Console.WriteLine("Order successfully placed! Hit enter to go back to menu.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// input/output for the process of looking up customers by name with all
        /// the validation taking place along the way and finally displaying
        /// the customers meeting the search parameters.
        /// </summary>
        public void CustomerSearch()
        {
            CustomerCreation validation = new CustomerCreation();
            Console.WriteLine("Please enter a customer's partial or full first name.");
            string firstName = Console.ReadLine();
            if (firstName == "cancel") { return; }

            // check if valid first name
            while (firstName != "" && !validation.IsValidName(firstName))
            {
                Console.WriteLine("Invalid first name, please enter another or an empty string.");
                firstName = Console.ReadLine();
                if (firstName == "cancel") { return; }
            }

            Console.WriteLine("Please enter a customer's partial or full last name.");
            string lastName = Console.ReadLine();
            if (lastName == "cancel") { return; }

            // check if valid last name
            while (lastName != "" && !validation.IsValidName(lastName))
            {
                Console.WriteLine("Invalid last name, please enter another or an empty string.");
                lastName = Console.ReadLine();
                if (lastName == "cancel") { return; }
            }

            CustomerQueries search = new CustomerQueries();
            var searchedCustomers = search.CustomerSearch(firstName, lastName);

            // check if any customers have this first/last name
            if (searchedCustomers.Count() == 0)
            {
                Console.WriteLine("There are no Customers matching the search parameters");
            }
            else
            {
                // display list of customers fitting the first/last name
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

        /// <summary>
        /// input/output for the process of displaying an order's details with all
        /// the validation taking place along the way and finally displaying
        /// the details of the order meeting the input parameters.
        /// </summary>
        public void DisplayOrderDetails()
        {
            OrderCreation createOrder = new OrderCreation(); // for Validation method
            OrderQueries checkOrder = new OrderQueries();
            int orderID;

            // get and display orders to pick from
            var orders = checkOrder.GetOrders();
            Console.WriteLine("ID\tTimestamp");
            foreach (var o in orders)
            {
                Console.WriteLine($"{o.OrderID}\t{o.Timestamp}");
            }

            Console.WriteLine("Please enter the ID of the order you would like to see");
            do
            {
                string input = Console.ReadLine();
                if (input == "cancel") { return; }

                // check if input is an int
                while (!createOrder.IsValidNum(input))
                {
                    Console.WriteLine("Invalid order ID number, please enter another.");
                    input = Console.ReadLine();
                    if (input == "cancel") { return; }
                }

                int id = createOrder.StringToInt(input);
                
                // check if there is an order with the given ID
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

            // get all the order details and display them to console
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

        /// <summary>
        /// input/output for the process of displaying an store's history with all
        /// the validation taking place along the way and finally displaying
        /// the history of the store meeting the input parameters.
        /// </summary>
        public void DisplayStoreHistory()
        {
            StoreQueries checkStore = new StoreQueries();
            OrderCreation checkNum = new OrderCreation();

            // get and display stores to pick from
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
                if (input == "cancel") { return; }

                // check if input is an int
                while (!checkNum.IsValidNum(input))
                {
                    Console.WriteLine("Invalid ID number, please enter another.");
                    input = Console.ReadLine();
                    if (input == "cancel") { return; }
                }

                int id = checkNum.StringToInt(input);

                // check if there is a store with the given ID
                if (checkStore.IsValidStoreID(id))
                {
                    storeID = id;
                }
                else
                {
                    Console.WriteLine("There is no store with this ID, please enter another.");
                    storeID = 0;
                }
            } while (storeID == 0); // repeat if no store with that ID

            var storeHistory = checkStore.GetStoreHistory(storeID);
            var storeLocation = checkStore.GetStoreLocation(storeID);
            
            // get and display all the order history for that location
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

        /// <summary>
        /// input/output for the process of displaying an customer's history with all
        /// the validation taking place along the way and finally displaying
        /// the history of the customer meeting the input parameters.
        /// </summary>
        public void DisplayCustomerHistory()
        {
            CustomerQueries checkCustomer = new CustomerQueries();
            OrderCreation checkNum = new OrderCreation();

            // get and display all the customer info to pick from
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
                if (input == "cancel") { return; }

                // check if input is an int
                while (!checkNum.IsValidNum(input))
                {
                    Console.WriteLine("Invalid ID number, please enter another.");
                    input = Console.ReadLine();
                    if (input == "cancel") { return; }
                }

                int id = checkNum.StringToInt(input);

                // check to see if there is a customer with the given ID
                if (checkCustomer.IsValidCustomerID(id))
                {
                    customerID = id;
                }
                else
                {
                    Console.WriteLine("There is no customer with this ID, please enter another.");
                    customerID = 0;
                }
            } while (customerID == 0); // repeat if no customer with that ID

            var customerHistory = checkCustomer.GetCustomerHistory(customerID);
            var customer = checkCustomer.GetCustomer(customerID);

            // get and display the order history of that customer if they have one
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
