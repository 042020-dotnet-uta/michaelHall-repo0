using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreApp.BusinessLogic
{
    public class CustomerCreation
    {
        public bool IsValidName(string name)
        {
            if (!Regex.Match(name, "^[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }
            return true;
        }

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
