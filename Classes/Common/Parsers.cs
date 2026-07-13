using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBol.Classes.Common
{
    public class Parsers
    {
        public static DateTime ParseDateString(string date)
        {
            var ci = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            return DateTime.Parse(date, ci);
        }
    }
}
