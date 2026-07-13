using System;
using System.IO;
using System.Text;
using System.Net;
using System.Reflection;
using SimpleBol.Properties;
using SimpleBol.Models;
using System.Net.Http;

namespace SimpleBol.Classes.Common
{
    public class HttpTest
    {
        static readonly HttpClient client = new();

        public static async Task<bool> TestHomePageAsync()
        {
            var pValue = false;
            var testUrl = Resources.AmazonUrl;

            try
            {
                using HttpResponseMessage response = await client.GetAsync(testUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Contains("<!DOCTYPE HTML>"))
                {
                    Global360.GOnline = true;
                    pValue = true;
                }
            }

            catch (WebException ex)
            {
                Global360.GOnline = false;
                Console.Write(ex.Message);
            }

            return pValue;
        }

        
    }

}

