using Newtonsoft.Json;
using System;

namespace GoAndSee_API.Models.DTO.Objects
{
    public class ObjectListDTO
    {
        [JsonProperty("id")]
        public string Oid { get; set; }
        [JsonProperty("name")]
        public string Oname { get; set; }
        public int OnumOfquestion { get; set; }
        [JsonProperty("lastAnswered")]
        public DateTime OlastAnswered { get; set; }
        [JsonProperty("lastModified")]
        public DateTime OlastModified { get; set; }
        [JsonProperty("created")]
        public DateTime Ocreated { get; set; }
        [JsonProperty("createdBy")]
        public string Ouserid { get; set; }
    }
}
