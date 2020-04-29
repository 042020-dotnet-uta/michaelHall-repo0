using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StoreApp
{
    class StoreApp_Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source = storeApp.db");
            }
        }
    }
}
