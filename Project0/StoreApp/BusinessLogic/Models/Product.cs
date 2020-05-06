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
        private Store store;        // Foreign Key
        private string productName;
        private int inventory;
        private double price;
        private ICollection<Order> orders;

        /// <summary>
        /// Property to get the productID Primary Key
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { ; }
        }

        /// <summary>
        /// Property to get and set the storeID Foreign Key
        /// </summary>
        public int StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }

        /// <summary>
        /// Property for storeID Foreign Key setup
        /// </summary>
        public Store Store
        {
            get { return store; }
            set { store = value; }
        }

        /// <summary>
        /// Property to get and set the product's name
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        /// <summary>
        /// Property to get and set the product's inventory at the respective store
        /// </summary>
        public int Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        /// <summary>
        /// Property to get and set the product's price
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// Property for Foreign Key setup in Orders
        /// </summary>
        public ICollection<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        #endregion
    }
}
