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
    }
}
