using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreApp
{
    public class Order
    {
        #region Fields & Properties
        private int orderID;   // Primary Key
        [ForeignKey("productID")]
        private int productID;
        [ForeignKey("customerID")]
        private int customerID;
        private int quantity;
        private DateTime orderTime;

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

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }
        #endregion
    }
}
