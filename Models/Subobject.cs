using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GoAndSee_API.Models
{
    public class Subobject
    {
        [Key]
        [JsonProperty("sid")]
        public string Sid { get; set; }
        [JsonProperty("objectid")]
        public string Sobjectid { get; set; }
        [JsonProperty("title")]
        public string Stitle { get; set; }
        [JsonProperty("ouserid")]
        public string Suserid { get; set; }

    }
}
