using System;
using SimpleBol.Classes.Errors;

namespace EventLogging
{
    public class FatalEvent
    {
        public static void ForceFatalException()
        {
            string causeOfFailure = "A catastrophic failure has occured.";

            try
            {
                Environment.FailFast(causeOfFailure);
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "Force Exception");
            }

            finally
            {
                Console.WriteLine("This finally block will not be executed.");
            }
        }

        public static void ForceException()
        {
            string causeOfFailure = "A catastrophic failure has occured.";

            try
            {
                throw new Exception(causeOfFailure);
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "Force Exception");
            }

            finally
            {
                Console.WriteLine("This finally block will not be executed.");
                throw new Exception(causeOfFailure);

            }
        }
    }
}
