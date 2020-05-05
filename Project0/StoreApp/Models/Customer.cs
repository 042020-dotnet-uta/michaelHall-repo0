using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreApp
{
    public class Customer
    {
        #region Fields & Properties
        
        private int customerID;     // CustomerID - PK
        private string firstName;
        private string lastName;
        private string userName;
        private ICollection<Order> orders;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public ICollection<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }
        #endregion

        #region Methods

        #endregion
    }
}