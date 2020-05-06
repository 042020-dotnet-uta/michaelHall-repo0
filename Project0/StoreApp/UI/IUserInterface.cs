using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.UI
{
    interface IUserInterface
    {
        // interface functions for the UI
        void StartApp();
        void MainMenu();
        bool IsValidMenuInput(string input);
        void AddNewCustomer();
        void AddNewOrder();
        void CustomerSearch();
        void DisplayOrderDetails();
        void DisplayStoreHistory();
        void DisplayCustomerHistory();
    }
}
