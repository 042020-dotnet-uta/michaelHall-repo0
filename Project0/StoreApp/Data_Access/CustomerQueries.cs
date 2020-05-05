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
    }
}
