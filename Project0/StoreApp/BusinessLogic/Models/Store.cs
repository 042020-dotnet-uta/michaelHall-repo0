using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    public class Store
    {
        #region Fields & Properties
        private int storeID;    // Primary Key
        private string location;
        private ICollection<Product> products;

        /// <summary>
        /// Property to get the storeID
        /// </summary>
        public int StoreID
        {
            get { return storeID; }
            set { ; }
        }

        /// <summary>
        /// Property to get and set the store's location
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Property for Foreign Key setup in Products
        /// </summary>
        public ICollection<Product> Products
        {
            get { return products; }
            set { products = value; }
        }
        #endregion
    }
}
