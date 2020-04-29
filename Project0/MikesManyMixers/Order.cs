using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp
{
    class Order
    {
        #region Fields & Properties
        // OrderID - PK
        // StoreID - FK
        // CustomerID - FK
        // list of ProductIDs 
        // list of ProductQuantities
        private DateTime orderTime;

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }
        #endregion
    }
}
