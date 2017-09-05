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
        private const string LastTenTweetsUrl = @"statuses/user_timeline.json?screen_name=salesforce&tweet_mode=extended&count=10&sort_by=created_at-desc";        

        public TweetRetriever(IRestClient restClient)
        {
            this.restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<IEnumerable<Tweet>> GetTweetAsync()
        {
            IEnumerable<Status> statuses = await restClient.GetAsync<IEnumerable<Status>>(LastTenTweetsUrl);

            return statuses.Select(MapToTweet);
        }

        private static Tweet MapToTweet(Status status)
        {
            string formattedMessage = status.FullText;

            foreach (var media in status.Entities?.Media ?? Enumerable.Empty<Media>())
            {
                formattedMessage = formattedMessage.Replace(media.Url, string.Empty);
            }

            foreach (var entityUrl in status.Entities?.Urls ?? Enumerable.Empty<EntityUrl>())
            {
                formattedMessage = formattedMessage.Replace(entityUrl.Url, $"<a href=\"{entityUrl.Url}\">{entityUrl.DisplayUrl}</a>");
            }

            return new Tweet
            {
                CreatedOn = DateTime.ParseExact(status.CreatedAt, "ddd MMM dd HH:mm:ss +ffff yyyy", CultureInfo.CurrentCulture),
                UserName = status.User.Name,
                UserScreenName = status.User.ScreenName,
                UserProfileImageUrl = status.User.ProfileImageUrl,
                Message = formattedMessage,
                ImageUrls = status.Entities?.Media?.Select(m => m.MediaUrl),
                RetweetCount = status.RetweetCount
            };
        }
    }
}
