using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApp.BusinessLogic
{
    public class OrderCreation
    {
        public bool IsValidUserName(string userName)
        {
            if (!Regex.Match(userName, "^(?=.{3,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").Success)
            {
                return false;
            }
            return true;
        }

        public bool IsValidNum(string input)
        {
            int anInt;
            if (!int.TryParse(input, out anInt)) // checks if it's not an int
            {
                return false;
            }
            else if (anInt < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int StringToInt(string input)
        {
            int anInt;
            if(!int.TryParse(input, out anInt))
            {
                return 0;
            }
            return anInt;
        }

        public bool IsUnreasonableQuantity(int amount)
        {
            if (amount >= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime GetTimeStamp()
        {
            DateTime timestamp = DateTime.Now;
            return timestamp;
        }
    }
}
