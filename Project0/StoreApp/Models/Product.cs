using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp
{
    class Product
    {
        #region Fields & Properties
        private int productID;      // Primary Key
        [ForeignKey("storeID")]
        private int storeID;
        private string productName;
        private int inventory;
        private int price;

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

        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        #endregion
    }
}
