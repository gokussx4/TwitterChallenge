using Feeder;
using Feeder.Models;
using Feeder.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;

namespace FeederTest
{
    public class TweetRetrieverTests
    {
        [Fact]
        public void GetTweetsHaveMessage()
        {
            var mockClientHandler = new MockHttpClientHandler();

            string fakeLastTenTweetsUrl = @"statuses/user_timeline.json?screen_name=salesforce&tweet_mode=extended&count=10&sort_by=created_at-desc";
            string fakeBaseUrl = @"https://api.twitter.com/1.1/";
            string fakeBearerToken = @"asdlkfjadsfoxijfvlkad";

            mockClientHandler.AddFakeResponse(new Uri(fakeBaseUrl + fakeLastTenTweetsUrl),
            new HttpResponseMessage(System.Net.HttpStatusCode.Accepted)
            {
                Content = new ObjectContent<Status[]>(new Status[]
                    {
                        new Status
                        {
                            FullText = "Testing",
                            CreatedAt = "Mon Sep 04 00:00:11 +0000 2017",
                            User = new User()
                        },
                        new Status
                        {
                            FullText = "Testing 2",
                            CreatedAt = "Mon Sep 04 00:00:11 +0000 2017",
                            User = new User()
                        }
                    }, new JsonMediaTypeFormatter())
            });

            IRestClient restClient = new RestClient(fakeBaseUrl, fakeBearerToken, mockClientHandler);

            TweetRetriever tweetRetriever = new TweetRetriever(restClient);

            Task<IEnumerable<Tweet>> tweets = tweetRetriever.GetTweetAsync();

            tweets.Wait();

            Assert.Collection(tweets.Result,
                (tweet) => tweet.Message.Contains("Testing"),
                (tweet) => tweet.Message.Contains("Testing 2"));
        }

        [Fact]
        public void TweetRetrieverWithNullRestClient_ArgumentNullExcpetion()
        {
            Assert.Throws<ArgumentNullException>(() => new TweetRetriever(null));
        }
    }
}
