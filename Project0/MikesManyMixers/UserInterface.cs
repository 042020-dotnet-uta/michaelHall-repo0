using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp
{
    class UserInterface
    {
        #region Fields & Properties

        #endregion

        #region Methods
        public void StartApp()
        {
            Console.WriteLine("Hello World!");
            Customer custom1 = new Customer();
            Console.WriteLine("Employee old name: " + custom1.FirstName);
            custom1.FirstName = "Dyne Smith";
            Console.WriteLine("Employee new name: " + custom1.FirstName);

            using (StoreApp_Context db = new StoreApp_Context())
            {
                Console.WriteLine("Inserting new Customer");
                db.Add(new Customer { FirstName = "Mike", LastName = "Hall" });
                db.SaveChanges();

                Console.WriteLine("Querying for a customer");
                var customer = db.Customers
                    .OrderBy(c => c.CustomerID)
                    .First();
                Console.WriteLine($"The customer is named: {customer.FirstName} {customer.LastName}");

                db.Add(new Customer { FirstName = "Nate", LastName = "Rich" });
                db.SaveChanges();
                var customer2 = db.Customers
                   .OrderBy(c => c.CustomerID);
                Console.WriteLine(customer2.Count());
                Console.WriteLine("Delete the Customer DB");
                //db.Remove(customer);
                Console.WriteLine(db.Customers);
                //db.SaveChanges();
                Console.WriteLine(customer.CustomerID);
                Console.WriteLine(custom1.CustomerID);

                
            }
        }
        #endregion
    }
}
