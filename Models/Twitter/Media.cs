using Newtonsoft.Json;

namespace Feeder.Models.Twitter
{
    public class Media
    {
        public int[] Indices { get; set; }

        [JsonProperty(PropertyName = "media_url")]
        public string MediaUrl { get; set; }

        public string Type { get; set; }
    }
}