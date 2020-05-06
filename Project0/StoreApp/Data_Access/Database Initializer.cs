using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data_Access
{
    class Database_Initializer
    {
        public void SetUpDatabase()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    db.Add(new Store { Location = "New York  " });
                    db.Add(new Store { Location = "Harrisburg" });
                    db.Add(new Store { Location = "Austin    " });
                    db.SaveChanges();

                    db.Add(new Product { ProductName = "Shampoo    ", StoreID = 1, Inventory = 15, Price = 6.50 });
                    db.Add(new Product { ProductName = "Conditioner", StoreID = 1, Inventory = 10, Price = 5.00 });
                    db.Add(new Product { ProductName = "Soap       ", StoreID = 1, Inventory = 20, Price = 4.00 });
                    db.Add(new Product { ProductName = "Shampoo    ", StoreID = 2, Inventory = 30, Price = 5.00 });
                    db.Add(new Product { ProductName = "Conditioner", StoreID = 2, Inventory = 20, Price = 4.00 });
                    db.Add(new Product { ProductName = "Soap       ", StoreID = 2, Inventory = 10, Price = 3.00 });
                    db.Add(new Product { ProductName = "Shampoo    ", StoreID = 3, Inventory = 15, Price = 4.00 });
                    db.Add(new Product { ProductName = "Conditioner", StoreID = 3, Inventory = 15, Price = 4.00 });
                    db.Add(new Product { ProductName = "Soap       ", StoreID = 3, Inventory = 30, Price = 2.00 });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                }
            }
        }
    }
}
