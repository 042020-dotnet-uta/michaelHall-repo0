using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    public class CustomerQueries
    {
        /// <summary>
        /// Queries through the Customers table and returns those that
        /// contain the first and last name parameters in them.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public ICollection<Customer> CustomerSearch(string first, string last)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                 /* This block of code can fail without the try/catch block:
                  
                    return db.Customers
                   .AsNoTracking()
                   .Where(c => c.FirstName.Contains(first) && c.LastName.Contains(last))
                   .OrderBy(c => c.FirstName)
                   .ToList();
                   */
                  
                try
                {
                    // find customers with the specified first and last names
                    return db.Customers
                   .AsNoTracking()
                   .Where(c => c.FirstName.Contains(first) && c.LastName.Contains(last))
                   .OrderBy(c => c.FirstName)
                   .ToList();
                }
                catch (Microsoft.Data.Sqlite.SqliteException)
                {
                    // exception for if there is not table
                    Console.WriteLine($"There is no customer table currently.");
                    return null;
                }
                catch (Exception e)
                {
                    // general exceptions
                    Console.WriteLine($"Exception occurred: {e}");
                    return null;
                }
            }
        }
        
        /// <summary>
        /// Queries through the Customers table and sees if the inputted
        /// customer ID is a real one or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsValidCustomerID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // find customers that have the inputted ID (if any)
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

        /// <summary>
        /// Queries through Customers table and returns the whole table
        /// </summary>
        /// <returns></returns>
        public ICollection<Customer> GetCustomers()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // get all the data from the Customers table
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

        /// <summary>
        /// Queries through the Customers table and extracts all the 
        /// order history and relative information for the customer
        /// with the matching customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Order> GetCustomerHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets all order data for the customer with the inputted ID
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

        /// <summary>
        /// Queries through the Customers table to get the customer that
        /// has the matching inputted ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets the customer info who has the inputted ID
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
