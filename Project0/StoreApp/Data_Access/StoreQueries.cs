using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    class StoreQueries
    {
        public ICollection<Store> GetStores()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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

        public void UpdateInventory(Order newOrder)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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

        public bool IsValidStoreID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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

        public ICollection<Order> GetStoreHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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

        public string GetStoreLocation(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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
