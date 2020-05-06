using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    class OrderQueries
    {
        public bool IsValidOrderID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
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

        public ICollection<Order> GetOrderDetails(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                try
                {
                    var order = db.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderID == id)
                    .FirstOrDefault();
                    var time = order.Timestamp;

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
    }
}
