using Feeder.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeederTest
{
    public class MockHttpClientHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> fakeResponses = new Dictionary<Uri, HttpResponseMessage>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage httpResponseMessage)
        {
            fakeResponses.Add(uri, httpResponseMessage);
        }
        
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(fakeResponses.ContainsKey(request.RequestUri))
            {
                return await Task.FromResult(fakeResponses[request.RequestUri]);
            }

            return await Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
        }
    }
}
