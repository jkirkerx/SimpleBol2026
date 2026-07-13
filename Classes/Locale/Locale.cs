using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Classes.Locale
{
    public class Locale
    {
        public static string GetRegionUnitOfMeasurement()
        {
            string pValue = "LBS";

            var regionInfo = RegionInfo.CurrentRegion;
            if (regionInfo != null)
            {
                if (regionInfo.IsMetric)
                {
                    pValue = "KG";
                }                
            }

            return pValue;

        }
    }
}
