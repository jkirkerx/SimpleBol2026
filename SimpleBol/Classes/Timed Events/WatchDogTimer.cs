using SimpleBol.Classes.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Classes.Timed_Events
{
    public class WatchDogTimer
    {
        public static bool OnlineCheck()
        {
            var pingSender = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };

            // Create a buffer of 32 bits of data to be transmitted
            var data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var buffer = Encoding.ASCII.GetBytes(data);
            var timeout = 120;

            try
            {
                var reply = pingSender.Send("8.8.8.8", timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    reply = pingSender.Send("1.1.1.1", timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                ErrorLogging.NLogException(ex, "WatchDogTimer");
                return false;
            }
        }

    }
}
