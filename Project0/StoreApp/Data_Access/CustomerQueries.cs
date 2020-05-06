using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    public class CustomerQueries
    {
        public ICollection<Customer> CustomerSearch(string first, string last)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                 /*return db.Customers
                   .AsNoTracking()
                   .Where(c => c.FirstName.Contains(first) && c.LastName.Contains(last))
                   .OrderBy(c => c.FirstName)
                   .ToList();*/
                  
                try
                {
                    return db.Customers
                   .AsNoTracking()
                   .Where(c => c.FirstName.Contains(first) && c.LastName.Contains(last))
                   .OrderBy(c => c.FirstName)
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
        
        public bool IsValidCustomerID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    var check = db.Customers
                    .Where(c => c.CustomerID == id);

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

        public ICollection<Customer> GetCustomers()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    return db.Customers
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

        public ICollection<Order> GetCustomerHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    return db.Orders
                    .AsNoTracking()
                    .Where(o => o.CustomerID == id)
                    .Include(customer => customer.Customer)
                    .Include(order => order.Product)
                    .ThenInclude(product => product.Store)
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

        public Customer GetCustomer(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    return db.Customers
                     .AsNoTracking()
                     .Where(c => c.CustomerID == id)
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
    }
}
