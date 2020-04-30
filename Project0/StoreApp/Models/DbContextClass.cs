using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StoreApp
{
    public class StoreApp_DbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreApp_DbContext() { }

        public StoreApp_DbContext(DbContextOptions<StoreApp_DbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring (DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source = storeApp.db");
            }
        }
    }
}
