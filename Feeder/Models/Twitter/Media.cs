using Newtonsoft.Json;

namespace Feeder.Models.Twitter
{
    public class Media
    { 
        [JsonProperty(PropertyName = "media_url")]
        public string MediaUrl { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}