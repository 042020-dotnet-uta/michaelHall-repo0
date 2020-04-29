using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    class Customer
    {
        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        #region Fields & Properties
        // CustomerID - PK
        private string firstName;
        private string lastName;
        private string userName;
        private string password;
        private string defaultStore;

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

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string DefaultStore
        {
            get { return defaultStore; }
            set { defaultStore = value; }
        }
        #endregion

        #region Methods

        #endregion
    }
}