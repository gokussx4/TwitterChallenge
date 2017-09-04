using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Feeder
{
    public class RestClient : IRestClient
    {
        private readonly Uri baseUri;
        private readonly DelegatingHandler mockHandler;

        public RestClient(string baseUrl) 
            : this(baseUrl, null)
        {

        }

        public RestClient(string baseUrl, DelegatingHandler mockHandler)
        {
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            if (!baseUrl.EndsWith('/'))
            {
                throw new InvalidOperationException($"{nameof(baseUrl)} must end with a trailing slash.");
            }

            baseUri = new Uri(baseUrl);
            this.mockHandler = mockHandler;
        }

        public async Task<T> GetAsync<T>(string relativeUrl, string bearerToken)
        {
            if (relativeUrl == null)
            {
                throw new ArgumentNullException(nameof(relativeUrl));
            }

            if (relativeUrl.StartsWith('/'))
            {
                throw new InvalidOperationException($"{nameof(relativeUrl)} must not begin with a slash.");
            }

            using (HttpClient httpClient = GetHttpClient())
            {
                if (!string.IsNullOrEmpty(bearerToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                string result = await httpClient.GetStringAsync(relativeUrl);

                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        private HttpClient GetHttpClient()
        {
            HttpClient httpClient;

            if(mockHandler != null)
            {
                httpClient = new HttpClient(mockHandler);
            }
            else
            {
                httpClient = new HttpClient();
            }            

            httpClient.BaseAddress = baseUri;

            return httpClient;
        }
    }
}
