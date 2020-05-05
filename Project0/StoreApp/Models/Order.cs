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
        private int orderID;   // Primary Key
        private int productID;
        private Product product;
        private int customerID;
        private Customer customer;
        private int quantity;
        private DateTime timestamp;

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }
        #endregion
    }
}
