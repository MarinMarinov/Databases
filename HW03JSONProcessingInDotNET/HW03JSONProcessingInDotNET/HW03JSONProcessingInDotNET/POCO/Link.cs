using Newtonsoft.Json;

namespace HW03JSONProcessingInDotNET.POCO
{
    public class Link
    {
        [JsonProperty("@href")]
        public string Href{ get; set;} 
    }
}