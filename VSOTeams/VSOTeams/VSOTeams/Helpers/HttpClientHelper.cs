using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VSOTeams.Helpers
{
    internal static class HttpClientHelper
    {

        internal static async Task<string> RequestVSO(string requestUri)
        {
            var _credentials = await LoginInfo.GetCredentials();
            var username = _credentials.UserName;
            var password = _credentials.Password;

            string uriString = string.Format("https://{0}.visualstudio.com", _credentials.Account);
            uriString = uriString + requestUri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", username, password))));

                HttpResponseMessage response = await client.GetAsync(uriString);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<bool> PostToVSO(string requestUri, HttpContent data)
        {
            var _credentials = await LoginInfo.GetCredentials();
            var username = _credentials.UserName;
            var password = _credentials.Password;

            string uriString = string.Format("https://{0}.visualstudio.com", _credentials.Account);
            uriString = uriString + requestUri;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", username, password))));

                HttpResponseMessage response = await client.PostAsync
                    (uriString, data);

                response.EnsureSuccessStatusCode();
                var test = await response.Content.ReadAsStringAsync();
            }

            return true;
        }
    }
}
