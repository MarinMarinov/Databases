using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Task11AndTask12
{
    class Program
    {
        static void Main()
        {
            string filePath = "../../../Tasks1-Task6AndTask8/catalogue.xml";

            // Task 11.
            // Write a program, which extract from the file catalog.xml the prices for all 
            // albums, published 5 years ago or earlier.

            Console.WriteLine("Task 11. Extract all albums, published 5 years ago or earlier");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);

            XmlNodeList albums = xmlDocument.SelectNodes("Catalogue/Albums/Album");

            var filteredAlbumsNamesAndPrice = new List<string>();

            foreach (XmlNode album in albums)
            {
                int year = int.Parse(album.SelectSingleNode("Year").InnerText);

                if (year <= DateTime.Now.Year - 5)
                {
                    filteredAlbumsNamesAndPrice.Add(string.Format("Name: {0}, Year: {1}, Price: {2}",
                        album.SelectSingleNode("Name").InnerText, year, album.SelectSingleNode("Price").InnerText));
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, filteredAlbumsNamesAndPrice));


            // Task 12.
            // Rewrite the previous using LINQ query.
            Console.WriteLine("Task 12. Extract all albums, published 5 years ago or earlier, using LINQ query");

            XDocument xDocument = XDocument.Load(filePath);

            var olderAlbums = from album in xDocument.Descendants("Album")
                where int.Parse(album.Element("Year").Value) <= (DateTime.Now.Year - 5)
                select new
                {
                    Name = album.Element("Name").Value,
                    Year = album.Element("Year").Value,
                    Price = album.Element("Price").Value
                };

            foreach (var album in olderAlbums)
            {
                Console.WriteLine("Name: {0}, Year: {1}, Price: {2}",
                        album.Name, album.Year, album.Price);
            }
        }
    }
}
