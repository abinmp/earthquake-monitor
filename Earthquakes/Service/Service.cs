using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Earthquakes
{
    internal static class Service
    {
        /// <summary>
        /// Get JSON result from a url
        /// </summary>
        /// <param name="uri">Url</param>
        /// <param name="retryCount">number of retry for failure</param>
        /// <returns></returns>
        public static JObject GetJsonData(string uri, int retryCount = 0)
        {
            var currRetry = 0;
            var secDelay = 1;
            for (;;)
            {
                try
                {
                    using (var webClient = new System.Net.WebClient())
                    {
                        return JObject.Parse(webClient.DownloadString(uri));
                    }
                }
                catch (Exception ex)
                {
                    currRetry++;
                    if (currRetry > retryCount)
                        return null;
                    Thread.Sleep(secDelay * 1000);
                }
            }
        }
    }
}
