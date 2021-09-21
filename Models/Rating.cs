using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoAndSee_API.Models
{
    public class Rating
    {
        //[Key]
        public string Rid { get; set; }
        [JsonProperty("subobjectid")]
        public string Rsoid { get; set; }
        [JsonProperty("objectid")]
        public string Roid { get; set; }
        //public string Ruserid { get; set; }
        //public DateTime Rtime { get; set; }
        [JsonProperty("rating")]
        public string Rrating { get; set; }
    }
}
