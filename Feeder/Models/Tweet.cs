using System;
using System.Collections.Generic;

namespace Feeder.Models
{
    public class Tweet
    {
        public DateTime CreatedOn { get; set; }
        public IEnumerable<string> ImageUrls { get; set; }
        public string Message { get; set; }
        public int RetweetCount { get; set; }
        public string UserName { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string UserScreenName { get; set; }
    }
}