using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApp.BusinessLogic
{
    public class OrderCreation
    {
        /// <summary>
        /// Checks to see if the inputted string is a valid username or not
        /// by using a regular expression statement where b/w 3-20 chars is good.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsValidUserName(string userName)
        {
            if (!Regex.Match(userName, "^(?=.{3,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").Success)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if the inputted string is able to be turned into
        /// an int or not by using a TryParse.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsValidNum(string input)
        {
            int anInt;
            if (!int.TryParse(input, out anInt)) // checks if it's not an int
            {
                return false;
            }
            else if (anInt <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Takes the inputted string and converts it to an int if 
        /// it is possible to do so, checks using TryParse. If not
        /// possible just returns 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int StringToInt(string input)
        {
            int anInt;
            if(!int.TryParse(input, out anInt))
            {
                return 0;
            }
            return anInt;
        }

        /// <summary>
        /// Sees if the inputted amount (amount of product being ordered) is
        /// reasonable or not, anything >= 10 is unreasonable.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the current date/time and returns it.
        /// </summary>
        /// <returns></returns>
        public DateTime GetTimeStamp()
        {
            DateTime timestamp = DateTime.Now;
            return timestamp;
        }
    }
}
