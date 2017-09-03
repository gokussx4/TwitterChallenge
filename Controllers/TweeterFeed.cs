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
        private const string baseTwitterUri = @"https://api.twitter.com/1.1/";
        
        [HttpGet]
        public async Task<IEnumerable<Tweet>> GetAsync()
        {
            TweetRetriever tweetRetriever = new TweetRetriever(new RestClient(baseTwitterUri));

            return await tweetRetriever.GetTweetAsync().ConfigureAwait(false);
        }
    }
}
