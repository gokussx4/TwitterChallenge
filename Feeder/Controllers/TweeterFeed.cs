using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Feeder.Models;

namespace Feeder
{    
    [Route("api/[controller]")]
    public class TweeterFeed : Controller
    {
        private const string BaseTwitterUrl = @"https://api.twitter.com/1.1/";
        private const string BearerToken = @"AAAAAAAAAAAAAAAAAAAAAGOg2AAAAAAA3ZlP4i%2FVqcp%2BFw84sVk29GJShWE%3DRJuyPmrTAIiudTS5lvQfOjminfWBE1H1pG7Mkp8UrSn8E6MSIM";

        [HttpGet]
        public async Task<IEnumerable<Tweet>> GetAsync()
        {
            TweetRetriever tweetRetriever = new TweetRetriever(new RestClient(BaseTwitterUrl, BearerToken));

            return await tweetRetriever.GetTweetAsync().ConfigureAwait(false);
        }
    }
}
