using Newtonsoft.Json;

namespace GoAndSee_API.Models
{
    public class Question
    {
        [JsonProperty("questionid")]
        public string QId { get; set; }
        [JsonProperty("objectid")]
        public string Objectid { get; set; }
        [JsonProperty("questions")]
        public string QName { get; set; }
        public string QUserid { get; set; }
        [JsonProperty("qcontact")]
        public string QContact { get; set; }
    }
}
