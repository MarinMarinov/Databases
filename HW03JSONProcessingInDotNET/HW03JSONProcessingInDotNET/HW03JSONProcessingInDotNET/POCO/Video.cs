using Newtonsoft.Json;
using System;

namespace HW03JSONProcessingInDotNET.POCO
{
    public class Video
    {
        [JsonProperty("yt:videoId")]
        public string Identifier { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("published")]
        public DateTime PublishedTime { get; set; }

        [JsonProperty("link")]
        public Link Link {get; set;}
    }
}