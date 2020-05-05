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
                return db.Stores
                    .AsNoTracking()
                    .ToList();
            }
        }

        public void UpdateInventory(Order newOrder)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                var product = db.Products
                    .Where(p => p.ProductID == newOrder.ProductID)
                    .FirstOrDefault();
                product.Inventory -= newOrder.Quantity;
                db.SaveChanges();
            }
        }

        public bool IsValidStoreID(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
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
        }

        public ICollection<Order> GetStoreHistory(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                return db.Orders
                    .AsNoTracking()
                    .Where(o => o.Product.StoreID == id)
                    .Include(customer => customer.Customer)
                    .Include(order => order.Product)
                    .OrderBy(o => o.Timestamp)
                    .ToList();
            }
        }

        public string GetStoreLocation(int id)
        {
            using (StoreApp_DbContext db = new StoreApp_DbContext())
            {
                var store = db.Stores
                     .AsNoTracking()
                     .Where(s => s.StoreID == id)
                     .FirstOrDefault();

                return store.Location;
            }
        }
    }
}
