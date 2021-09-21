using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GoAndSee_API.Models
{
    public class Object
    {
        [Key]
        public string Oid { get; set; }
        [JsonProperty("name")]
        public string Oname { get; set; }
        [JsonProperty("interval")]
        public int Ointerval { get; set; }
        [JsonProperty("description")]
        public string Odescription { get; set; }
        [JsonProperty("questions")]
        public string Oquestions { get; set; }
        public string Ouserid { get; set; }
        [JsonProperty("contact")]
        public string Ocontact { get; set; }
    }
}
