using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleBol.Classes.Common
{
    public class RegEx
    {
        public static bool ValidCurrency(string text)
        {
            Regex money = new Regex(@"(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        public static bool ValidNumber(string text)
        {
            Regex number = new Regex(@"(?<=\s|^)\d+(?=\s|$)");
            return number.IsMatch(text);
        }
    }
}
