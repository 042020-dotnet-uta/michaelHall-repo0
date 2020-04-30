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
            using (StoreApp_Context db = new StoreApp_Context())
            {

                /*Console.WriteLine("Inserting new Customer");
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
                db.SaveChanges();
                Console.WriteLine(customer.CustomerID);

                Store riteaid = new Store { Location = "RiteAid"};
                db.Add<Store>(riteaid);
                //db.Stores.Remove(db.Stores.First());
                db.SaveChanges();
                Product soap = new Product { Inventory = 2, Price = 4, ProductName = "soap", StoreID = 2};
                db.Add<Product>(soap);
                db.SaveChanges();
                */
            }
        }
        #endregion
    }
}
