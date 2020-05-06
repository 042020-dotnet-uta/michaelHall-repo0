using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    public class ProductQueries
    {
        /// <summary>
        /// Queries through products and returns all the product data
        /// (the entire table)
        /// </summary>
        /// <returns></returns>
        public ICollection<Product> GetProducts()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets all the product data from the table
                    return db.Products
                    .AsNoTracking()
                    .Include(p => p.Store)
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
        /// Checks to see if there is any product in the product table
        /// with the given productID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsValidProductID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get any products with the given productID
                    var check = db.Products
                    .Where(c => c.ProductID == id);

                    // get the inventory for that particular product
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

        /// <summary>
        /// Returns the product info for the product that has
        /// the given productID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductName(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets single product info for product with matching ID
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

        /// <summary>
        /// Checks to see if there are any products with the given
        /// productID and returns false if there are none but true if
        /// there are
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsValidProductQuantity(int amount, int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get product with the matching productID
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
