using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreApp
{
    public class Order
    {
        #region Fields & Properties
        private int orderID;        // Primary Key
        private int productID;      // Foreign Key
        private Product product;
        private int customerID;     // Foreign Key
        private Customer customer;
        private int quantity;
        private DateTime timestamp;

        /// <summary>
        /// OrderID property to get the order's ID
        /// </summary>
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        /// <summary>
        /// Property to get and set the productID Foreign Key
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        /// <summary>
        /// Property for productID Foreign Key setup
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        /// <summary>
        /// Property to get and set the customerID Foreign Key
        /// </summary>
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        /// <summary>
        /// Property for customerID Foreign Key setup
        /// </summary>
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        /// <summary>
        /// Property to get and set the order's quantity of product
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Property to get and set the order's timestamp
        /// </summary>
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        #endregion
    }
}
