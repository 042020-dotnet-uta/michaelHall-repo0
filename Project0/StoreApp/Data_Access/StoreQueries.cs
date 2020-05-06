using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    class StoreQueries
    {
        /// <summary>
        /// Returns all the store data within the store table.
        /// </summary>
        /// <returns></returns>
        public ICollection<Store> GetStores()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets all the store data from the table
                    return db.Stores
                    .AsNoTracking()
                    .ToList();
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    Console.WriteLine($"There is no customer table currently.");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Takes away the respective amount of product from the respective
        /// store's inventory when an order is successfully placed.
        /// </summary>
        /// <param name="newOrder"></param>
        public void UpdateInventory(Order newOrder)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get the inventory for a store's product and update it
                    // based on the given order that was just placed
                    var product = db.Products
                    .Where(p => p.ProductID == newOrder.ProductID)
                    .FirstOrDefault();
                    product.Inventory -= newOrder.Quantity;
                    db.SaveChanges();
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    Console.WriteLine($"There is no customer table currently.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                }
            }
        }

        /// <summary>
        /// Checks to see if the given storeID is a valid one or not.
        /// Returns true if it is and false if not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsValidStoreID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get any store with the matching storeID
                    var check = db.Stores
                    .AsNoTracking()
                    .Where(s => s.StoreID == id);

                    if (check.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    Console.WriteLine($"There is no customer table currently.");
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns the history of all the orders made from a particular
        /// store with the matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Order> GetStoreHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get all the order data for the store with the
                    // matching store ID
                    return db.Orders
                    .AsNoTracking()
                    .Where(o => o.Product.StoreID == id)
                    .Include(customer => customer.Customer)
                    .Include(order => order.Product)
                    .OrderBy(o => o.Timestamp)
                    .ToList();
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    Console.WriteLine($"There is no customer table currently.");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the store info for the store matching the given store ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetStoreLocation(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get store info for store with matching store ID
                    var store = db.Stores
                     .AsNoTracking()
                     .Where(s => s.StoreID == id)
                     .FirstOrDefault();

                    return store.Location;
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    Console.WriteLine($"There is no customer table currently.");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e}");
                    return null;
                }
            }
        }
    }
}
