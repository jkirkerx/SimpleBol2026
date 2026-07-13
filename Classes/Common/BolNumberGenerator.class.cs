using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Classes.Common
{
    internal class BolNumberGenerator
    {

        public static string GenerateBOLNumber()
        {
            // Get current timestamp
            DateTime currentDateTime = DateTime.Now;
            string timestamp = currentDateTime.ToString("yyMMddHHmm");

            // Generate a random number
            Random random = new Random();
            int randomNumber = random.Next(8, 99); // Adjusted range for 2-digit number

            // Generate a random capital letter
            char randomLetter = (char)random.Next('A', 'Z' + 1);

            // Combine truncated timestamp, random number, and letter
            string bolNumber = "BOL" + timestamp + randomLetter + randomNumber.ToString();

            return bolNumber;
        }

    }
}
