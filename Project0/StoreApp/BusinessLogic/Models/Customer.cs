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

        /// <summary>
        /// CustomerID property to get customer's ID
        /// </summary>
        public int CustomerID
        {
            get { return customerID; }
            set { ; }
        }

        /// <summary>
        /// FirstName property to get and set customer's firstName
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// LastName property to get and set customer's lastName
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /// <summary>
        /// UserName property to get and set customer's userName
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
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