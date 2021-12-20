using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Helpers.HttpRequestHelper
{
    public class HttpClientHelper
    {
        public static async Task<string> GetAsync(HttpClient client, string requestUri)
        {
            var result = string.Empty;
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return result;
        }

        public static async Task<string> PostAsync(HttpClient client, string requestUri, HttpContent content)
        {
            var result = string.Empty;
            var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return result;
        }
    }
}
