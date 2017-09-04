using Newtonsoft.Json;
using System.Collections.Generic;

namespace Feeder.Models.Twitter
{
    public class EntityUrl
    {
        public string Url { get; set; }
        [JsonProperty(PropertyName = "display_url")]
        public string DisplayUrl { get; internal set; }
    }
}