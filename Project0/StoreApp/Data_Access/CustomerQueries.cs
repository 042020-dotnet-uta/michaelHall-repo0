using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StoreApp.Data_Access
{
    class CustomerQueries
    {
        public ICollection<Customer> CustomerSearch(string first, string last)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                return db.Customers
                    .AsNoTracking()
                    .Where(c => c.FirstName.Contains(first) && c.LastName.Contains(last))
                    .OrderBy(c => c.FirstName)
                    .ToList();
            }
        }
        
        public bool IsValidCustomerID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
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
        }

        public ICollection<Customer> GetCustomers()
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                return db.Customers
                    .AsNoTracking()
                    .ToList();
            }
        }

        public ICollection<Order> GetCustomerHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
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
        }

        public Customer GetCustomer(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                return db.Customers
                     .AsNoTracking()
                     .Where(c => c.CustomerID == id)
                     .FirstOrDefault();
            }
        }
    }
}
