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
        }

        public ICollection<Order> GetOrderDetails(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
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
        }
    }
}
