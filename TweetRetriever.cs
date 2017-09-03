using Feeder.Models;
using Feeder.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Feeder
{
    public class TweetRetriever
    {
        private readonly IRestClient restClient;
        private const string LastTenTweetsUrl = @"statuses/user_timeline.json?screen_name=salesforce&tweet_mode=extended&count=10";
        private const string BearerToken = @"AAAAAAAAAAAAAAAAAAAAAGOg2AAAAAAA3ZlP4i%2FVqcp%2BFw84sVk29GJShWE%3DRJuyPmrTAIiudTS5lvQfOjminfWBE1H1pG7Mkp8UrSn8E6MSIM";

        public TweetRetriever(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<IEnumerable<Tweet>> GetTweetAsync()
        {
            IEnumerable<Status> statuses = await restClient.GetAsync<IEnumerable<Status>>(LastTenTweetsUrl, BearerToken);

            return statuses.Select(MapToTweet);
        }

        private static Tweet MapToTweet(Status s)
        {
            return new Tweet
            {
                CreatedOn = DateTime.ParseExact(s.CreatedAt, "ddd MMM dd HH:mm:ss +ffff yyyy", CultureInfo.CurrentCulture),
                UserName = s.User.Name,
                UserScreenName = s.User.ScreenName,
                UserProfileImageUrl = s.User.ProfileImageUrl,
                Message = s.FullText,
                ImageUrls = s.Entities?.Media?.Select(m => m.MediaUrl)
            };
        }
    }
}
