using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace VSOTeams.Helpers
{
    internal static class HttpClientHelper
    {
        internal static void CreateHttpClient(ref HttpClient httpClient)
        {
            LoginInfo _credentials = new LoginInfo();

            if (httpClient != null)
            {
                httpClient.Dispose();
            }

        }
    }
}
