using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp
{
    public class Product
    {
        #region Fields & Properties
        private int productID;      // Primary Key
        private int storeID;
        private Store store;
        private string productName;
        private int inventory;
        private double price;
        private ICollection<Order> orders;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public int StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }

        public Store Store
        {
            get { return store; }
            set { store = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public int Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public ICollection<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        #endregion
    }
}
