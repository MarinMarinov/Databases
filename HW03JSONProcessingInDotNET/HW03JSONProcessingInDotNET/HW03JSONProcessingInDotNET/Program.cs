using HW03JSONProcessingInDotNET.POCO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace HW03JSONProcessingInDotNET
{
    internal class Program
    {
        private static void Main()
        {
            // Task 1. The RSS feed is located at https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw

            string rssFeedUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            string fileXmlPath = "../../rssFeed.xml";
            string fileJsonPath = "../../rssFeed.json";


            // Task 2. Download the content of the feed programatically
            // You can use WebClient.DownloadFile()

            WebClient webClient = new WebClient();
            webClient.DownloadFile(rssFeedUrl, fileXmlPath);
            Console.WriteLine("File has been dawnloaded at {0}", Path.GetFullPath(fileXmlPath));
            Console.WriteLine(new string('-', 50));

            // Task 3. Parse the XML from the feed to JSON

            XDocument xDocument = XDocument.Load(fileXmlPath);
            string jsonDoc = JsonConvert.SerializeXNode(xDocument, Formatting.Indented);
            File.WriteAllText(fileJsonPath, jsonDoc);
            Console.WriteLine("RSS feed has been saved at {0}", Path.GetFullPath(fileXmlPath));
            //Console.WriteLine(jsonDoc);
            Console.WriteLine(new string('-', 50));


            // Task 4. Using LINQ-to-JSON select all the video titles and print the on the console

            JObject jsonObject = JObject.Parse(File.ReadAllText(fileJsonPath));
            IEnumerable<JToken> videoTitles = jsonObject["feed"]["entry"].Select(element => element["title"]);
            Console.WriteLine(string.Join(Environment.NewLine, videoTitles));
            Console.WriteLine(new string('-', 50));


            // Task 5. Parse the videos' JSON to POCO

            List<Video> videos =
                jsonObject["feed"]["entry"]
                .Select(obj => JsonConvert.DeserializeObject<Video>(obj.ToString()))
                .ToList();

            foreach (var video in videos)
            {
                Console.WriteLine(video.Author.Name);
                Console.WriteLine(video.Author.Uri);
                Console.WriteLine(video.Identifier);
                Console.WriteLine(video.Link.Href);
                Console.WriteLine(video.PublishedTime);
                Console.WriteLine(video.Title);
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 50));

            // Task 6. Using the POCOs create a HTML page that shows all videos from the RSS
            // Use <iframe>
            // Provide a links, that nagivate to their videos in YouTube

            //XElement html = new XElement("html", new XElement("head"));
            XElement html = new XElement("html");
            XElement body = new XElement("body");
            //XElement containerDiv = new XElement("div", "List with all videos");
            XElement ul = new XElement("ul");

            foreach (var video in videos)
            {
                string embededUrl = string.Format("http://www.youtube.com/embed/{0}?autoplay=0", video.Identifier);
                XElement li = new XElement("li", new XElement("p", video.Title));
                XElement iframe = new XElement("iframe", new XAttribute("src", embededUrl), "This is Iframe");
                li.Add(iframe);
                li.Add(new XElement("a", new XAttribute("href", video.Link.Href), "Link for YouTube video"));

                ul.Add(li);
            }

            //containerDiv.Add(ul);
            //body.Add(containerDiv);
            body.Add(ul);
            html.Add(body);

            html.Save("../../videos.html");
        }
    }
}
