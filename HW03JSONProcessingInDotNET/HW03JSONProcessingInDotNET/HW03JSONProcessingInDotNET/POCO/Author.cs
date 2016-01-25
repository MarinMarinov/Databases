using Newtonsoft.Json;

namespace HW03JSONProcessingInDotNET.POCO
{
    public class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}