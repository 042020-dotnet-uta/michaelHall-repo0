using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    public class ProductQueries
    {
        public ICollection<Product> GetProducts()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    return db.Products
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

        public bool IsValidProductID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    var check = db.Products
                    .Where(c => c.ProductID == id);

                    var inventoryCheck = db.Products
                        .AsNoTracking()
                        .Where(c => c.ProductID == id)
                        .FirstOrDefault();
                    if (check.Count() == 0 || inventoryCheck.Inventory == 0)
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

        public Product GetProductName(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    return db.Products
                    .AsNoTracking()
                    .Where(p => p.ProductID == id)
                    .FirstOrDefault();
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

        public bool IsValidProductQuantity(int amount, int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    var check = db.Products
                   .Where(p => p.ProductID == id)
                   .FirstOrDefault();
                    if (check.Inventory < amount)
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
    }
}
