using Newtonsoft.Json;

namespace GoAndSee_API.Models
{
    public class RatingAnswerDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("attachment")]
        public string Attachment { get; set; }
        [JsonProperty("contenttype")]
        public string Contenttype { get; set; }
    }
}
