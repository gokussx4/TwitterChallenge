using Newtonsoft.Json;

namespace Feeder.Models.Twitter
{
    public class User
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageUrl { get; set; }
    }
}