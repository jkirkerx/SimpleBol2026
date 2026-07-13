using System;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using NLog;
using SimpleBol.Models;

namespace SimpleBol.Classes.Errors
{
    public class ErrorLogging
    {

        public static void EvError(Exception ex, string className, string functionName, string specialClause)
        {
            string strVersion = AppInfo.Version;
            string eM = className + Environment.NewLine;
            eM += "Version: " + strVersion + Environment.NewLine;
            eM += "Function: " + functionName;
            eM += "Clause: " + specialClause;
            eM += "Exception: " + ex.Message + Environment.NewLine;

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = AppInfo.Name + " " + strVersion;
                eventLog.WriteEntry(eM, EventLogEntryType.Error, 3, Convert.ToInt16(100));
            }
        }

        public static void EvWarning(string className, string functionName, string specialClause)
        {
            string strVersion = AppInfo.Version;
            string eM = className + Environment.NewLine;
            eM += "Version: " + strVersion + Environment.NewLine;
            eM += "Function: " + functionName;
            eM += "Clause: " + specialClause;

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = AppInfo.Name + " " + strVersion;
                eventLog.WriteEntry(eM, EventLogEntryType.Warning, 3, Convert.ToInt16(100));
            }
        }

        public static void EvInformation(string message)
        {
            string strVersion = AppInfo.Version;
            string eM = "Version: " + strVersion + Environment.NewLine;
            eM += "Message: " + message;

            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = AppInfo.Name + " " + strVersion;
                eventLog.WriteEntry(eM, EventLogEntryType.Information, 3, Convert.ToInt16(100));
            }
        }

        public static void NLogUnhandled(Exception ex)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Fatal(ex);
        }

        public static void NLogException(Exception ex, string className)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(ex, className);
        }

        public static void NLogInformation(string message)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info(message);
        }

        public static NLogObject[] DeserializeNLogObject(string jsonResult)
        {
            var jsonArray = "[";
            var idx = 0;

            var jsonResults = jsonResult.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var result in jsonResults)
            {
                idx += 1;
                jsonArray += result;
                if ((idx < jsonResults.Length - 1))
                    jsonArray += ",";
            }
            jsonArray += "]";

            return JsonConvert.DeserializeObject<NLogObject[]>(jsonArray)!;
        }

        public static NLogNestedMessage DeserializeNLogNestedMessage(string jsonResult)
        {
            return JsonConvert.DeserializeObject<NLogNestedMessage>(jsonResult)!;
        }

        public static NLogNestedException DeserializeNLogNestedException(string jsonResult)
        {
            return JsonConvert.DeserializeObject<NLogNestedException>(jsonResult)!;
        }
    }
}
