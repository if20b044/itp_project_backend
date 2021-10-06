using Newtonsoft.Json;
using System;

namespace GoAndSee_API.Models.DTO.Objects
{
    public class ObjectDTO
    {
        [JsonProperty("id")]
        public string Oid { get; set; }
        [JsonProperty("name")]
        public string Oname { get; set; }
        [JsonProperty("description")]
        public string Odescription { get; set; }
        [JsonProperty("interval")]
        public int Ointerval { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Ocreated { get; set; }
        [JsonProperty("questions")]
        public string Oquestions { get; set; }
        [JsonProperty("subobjekte")]
        public string Osubobjects { get; set; }

    }
}
