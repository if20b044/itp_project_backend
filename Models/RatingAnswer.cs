using Newtonsoft.Json;

namespace GoAndSee_API.Models
{
    public class RatingAnswer
    {
        //[JsonProperty("id")]
        //public string Raquestionid { get; set; }
        public string Rarid { get; set; }
        public string Raquestion { get; set; }
        [JsonProperty("rating")]
        public int Rarating { get; set; }
        [JsonProperty("comment")]
        public string Racomment { get; set; }
        [JsonProperty("attachment")]
        public string Raattachment { get; set; }
        [JsonProperty("contenttype")]
        public string Raimagetype { get; set; }
    }
}
