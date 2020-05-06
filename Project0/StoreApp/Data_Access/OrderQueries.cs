using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    class OrderQueries
    {
        /// <summary>
        /// Queries through the Orders table and sees if the inputted
        /// order ID is a real one or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsValidOrderID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // checks to see if any orders have the specified ID
                    var check = db.Orders
                   .Where(o => o.OrderID == id);
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
        /// Queries through the orders and retrieves all the order 
        /// data for the order matching the given orderID. Also returns
        /// any orders that had the same timestamp meaning they were a part
        /// of the order as an order with multiple products.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Order> GetOrderDetails(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets order details for the order with the matching orderID
                    var order = db.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderID == id)
                    .FirstOrDefault();
                    var time = order.Timestamp;

                    // returns all orders with the specified timestamp
                    // for the case that multiple products are in the same order
                    return db.Orders
                        .AsNoTracking()
                        .Where(o => o.Timestamp == time)
                        .Include(customer => customer.Customer)
                        .Include(order => order.Product)
                        .ThenInclude(product => product.Store)
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
        /// Queries through orders and returns all the order data without
        /// the product and store and customer data
        /// (the entire table)
        /// </summary>
        /// <returns></returns>
        public ICollection<Order> GetOrders()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    // gets order data from the table (excludes foreign key data)
                    return db.Orders
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
    }
}
