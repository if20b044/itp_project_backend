using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoAndSee_API.Models
{
    public class RatingDTO
    {
        [JsonProperty("id")]
        public string Rid { get; set; }
        [JsonProperty("subobject")]
        public string Rsid { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Rtimestamp { get; set; }
        [JsonProperty("questions")]
        public List<RatingAnswerDTO> RQuestions { get; set; }
    }
}
