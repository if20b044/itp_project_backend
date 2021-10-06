using Newtonsoft.Json;

namespace GoAndSee_API.Models
{
    public class Rating
    {
        public string Rid { get; set; }
        [JsonProperty("rsoid")]
        public string Rsoid { get; set; }
        [JsonProperty("roid")]
        public string Roid { get; set; }
        [JsonProperty("rating")]
        public string Rrating { get; set; }
    }
}
