﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    public class Store
    {
        #region Fields & Properties
        private int storeID;    // Primary Key
        private string location;

        public int StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        #endregion

        #region Methods
       /* public override string ToString()
        {
         
        }*/
        #endregion
    }
}
