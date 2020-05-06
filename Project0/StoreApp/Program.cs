using System;
using StoreApp.UI;

namespace StoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            // declare the User Interface and run the program
            UserInterface ui = new UserInterface();
            ui.StartApp();
        }
    }
}
