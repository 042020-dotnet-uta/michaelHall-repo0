using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApp.BusinessLogic
{
    public class CustomerCreation
    {
        /// <summary>
        /// Checks to see if the inputted string is a valid name or not
        /// by using a regular expression statement.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsValidName(string name)
        {
            if (!Regex.Match(name, "^[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if the inputted string is a valid name or not
        /// by using a regular expression statement.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsValidInputName(string name)
        {
            if (!Regex.Match(name, "^(?=.{2,20}$)[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }
            return true;
        }

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
    }
}
