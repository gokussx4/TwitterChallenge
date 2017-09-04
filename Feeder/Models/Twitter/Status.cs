using Newtonsoft.Json;
using System.Collections.Generic;

namespace Feeder.Models.Twitter
{
    public class Status
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        public Entities Entities { get; set; }

        [JsonProperty(PropertyName = "full_text")]
        public string FullText { get; set; }

        [JsonProperty(PropertyName = "retweet_count")]
        public int RetweetCount { get; set; }

        public User User { get; set; }

        [JsonProperty(PropertyName = "display_text_range")]
        public IEnumerable<int> DisplayTextRange { get; set; }
    }
}