using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Tasks1_Task6AndTask8
{
    class Program
    {
        private const string CatalogueFilePath = "../../catalogue.xml";

        static void Main(string[] args)
        {
            AlbumFactory albumMaker = new AlbumFactory();

            Album firstAlbum = albumMaker.CreateAlbum("The Best Album", "Ivan The Singer", 2015, "Pesho The Producer", 20, 8);
            Album secondAlbum = albumMaker.CreateAlbum("The Worst Album", "Petkan Petkanov", 2005, "Gosho Producing company", 21, 10);
            Album thirdAlbum = albumMaker.CreateAlbum("Golden Album", "Dragana Draganovich", 2013, "Serbian Productions company", 18, 8);
            Album fourthAlbum = albumMaker.CreateAlbum("Platinum Album", "Dragana Draganovich", 2015, "Serbian Productions company", 22, 11);
            Album fifthAlbum = albumMaker.CreateAlbum("Just an Album", "John Smith", 2008, "Sony Music company", 19, 7);

            Catalogue catalogue = new Catalogue();

            catalogue.AddAlbum(firstAlbum);
            catalogue.AddAlbum(secondAlbum);
            catalogue.AddAlbum(thirdAlbum);
            catalogue.AddAlbum(fourthAlbum);
            catalogue.AddAlbum(fifthAlbum);

            ExecuteTask1(catalogue);
            ExecuteTask2();
            ExecuteTask3();
            ExecuteTask4();
            ExecuteTask5();
            ExecuteTask6();
            ExecuteTask8();

        }

        // Task 1 Create a XML file representing catalogue
        private static void ExecuteTask1(Catalogue catalogue)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Catalogue));


            StreamWriter streamWriter = new StreamWriter(CatalogueFilePath);
            xmlSerializer.Serialize(streamWriter, catalogue);
            streamWriter.Close();

            // Printing the generated XML file to the console

            Console.WriteLine("Task 1. The XML file");

            using (StreamReader reader = new StreamReader(CatalogueFilePath))
            {
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }
            }

            Console.WriteLine(new string('-', 50));
        }

        // Task 2 Write program that extracts all different artists which are found in the catalog.xml.

        private static void ExecuteTask2()
        {
            Console.WriteLine("Task 2 - Extracting all artists from catalog.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(CatalogueFilePath);

            XmlElement rootElement = doc.DocumentElement;

            XmlNodeList artists = rootElement.GetElementsByTagName("Artist");

            Dictionary<string, int> uniqueArtists = GetUniqueArtists(artists);

            foreach (KeyValuePair<string, int> artist in uniqueArtists)
            {
                Console.WriteLine("Artist name: {0}, number of albums: {1}", artist.Key, artist.Value);
            }

            Console.WriteLine(new string('-', 50));
        }

        // Task 3 Implement the previous using XPath
        private static void ExecuteTask3()
        {
            Console.WriteLine("Task 3 - Extracting all artists from catalog.xml using XPath");

            XmlDocument doc = new XmlDocument();
            doc.Load(CatalogueFilePath);

            XmlElement rootElement = doc.DocumentElement;

            XmlNodeList artists = rootElement.SelectNodes("Albums/Album/Artist");

            Dictionary<string, int> uniqueArtists = GetUniqueArtists(artists);

            foreach (KeyValuePair<string, int> artist in uniqueArtists)
            {
                Console.WriteLine("Artist name: {0}, number of albums: {1}", artist.Key, artist.Value);
            }

            Console.WriteLine(new string('-', 50));
        }

        // Task 4 Using the DOM parser write a program to delete from catalog.xml all albums having price > 20
        private static void ExecuteTask4()
        {
            Console.WriteLine("Task 4 - Removing all albums having price > 20.");

            XmlDocument doc = new XmlDocument();
            doc.Load(CatalogueFilePath);

            XmlElement rootElement = doc.DocumentElement;

            XmlElement albumsTag = (XmlElement)rootElement.FirstChild;
            RemoveAlbumsByPrice(20m, albumsTag);

            XmlNodeList remainingAlbums = rootElement.GetElementsByTagName("Album");

            string albumName = String.Empty;
            decimal albumPrice = 0.0m;

            foreach (XmlElement album in remainingAlbums)
            {
                albumName = album.GetElementsByTagName("Name")[0].InnerText;
                albumPrice = decimal.Parse(album.GetElementsByTagName("Price")[0].InnerText);
                Console.WriteLine("Album name: {0}, price: {1}", albumName, albumPrice);
            }

            Console.WriteLine(new string('-', 50));
        }
        // Task 5 Write a program, which using XmlReader extracts all song titles from catalog.xml.
        public static void ExecuteTask5()
        {
            Console.WriteLine("Task 5: Extracts all song titles using XmlReader.");

            List<string> allSongsNames = new List<string>();
            XmlReader reader = XmlReader.Create(CatalogueFilePath);

            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Song")
                    {
                        do
                        {
                            reader.Read();
                        } while (reader.Name != "Name");

                        string songName = reader.ReadElementString();

                        allSongsNames.Add(songName);
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, allSongsNames));

            Console.WriteLine(new string('-', 50));
        }

        // Task 6 Rewrite the same using XDocument and LINQ query
        private static void ExecuteTask6()
        {
            Console.WriteLine("Task 6: Extracts all song titles using XDocument and LINQ query.");

            XDocument xDocument = XDocument.Load(CatalogueFilePath);
            IEnumerable<XElement> allSongsXElements = xDocument.Descendants("Song");

            IEnumerable<string> allSongsNames = from song in allSongsXElements
                                select song.Element("Name").Value;

            Console.WriteLine(string.Join(Environment.NewLine, allSongsNames));

            Console.WriteLine(new string('-', 50));
        }

        // Task 8
        // Write a program, which (using XmlReader and XmlWriter) reads the file catalog.xml and creates the file albums.xml, 
        // in which stores in appropriate way the names of all albums and their authors.
        private static void ExecuteTask8()
        {
            Console.WriteLine("Task 8: Stores in albums.xml the names of all albums and their authors.");

            XmlTextWriter writer = new XmlTextWriter("../../albums.xml", Encoding.UTF8);
            XmlReader reader = XmlReader.Create(CatalogueFilePath);

            using (writer)
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 2;

                writer.WriteStartDocument();
                writer.WriteStartElement("Albums");

                using (reader)
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Album")
                        {
                            writer.WriteStartElement("Album");

                            do
                            {

                            } while (reader.Read() && reader.Name != "Name");
                            writer.WriteElementString("Name", reader.ReadInnerXml());

                            do
                            {

                            } while (reader.Read() && reader.Name != "Artist");
                            writer.WriteElementString("Artist", reader.ReadInnerXml());

                            writer.WriteEndElement();
                        }
                    }
                }

                writer.WriteEndDocument();
            }

            using (StreamReader streamReader = new StreamReader("../../albums.xml"))
            {
                while (!streamReader.EndOfStream)
                {
                    Console.WriteLine(streamReader.ReadLine());
                }
            }

            Console.WriteLine(new string('-', 50));
        }

        private static void RemoveAlbumsByPrice(decimal maxPrice, XmlElement parentElement)
        {
            XmlNodeList albumElements = parentElement.GetElementsByTagName("Album");
            List<XmlElement> albumsToRemove = new List<XmlElement>();

            foreach (XmlElement album in albumElements)
            {
                decimal price = decimal.Parse(album.GetElementsByTagName("Price")[0].InnerText);

                if (price > maxPrice)
                {
                    albumsToRemove.Add(album);
                }
            }

            foreach (XmlElement album in albumsToRemove)
            {
                album.ParentNode.RemoveChild(album);
            }
        }

        private static Dictionary<string, int> GetUniqueArtists(XmlNodeList xmlNodeList)
        {
            Dictionary<string, int> dictionaryOfArtists = new Dictionary<string, int>();

            string currentArtistName = String.Empty;

            foreach (XmlNode artist in xmlNodeList)
            {
                currentArtistName = artist.InnerText;

                if (dictionaryOfArtists.ContainsKey(currentArtistName))
                {
                    dictionaryOfArtists[currentArtistName] += 1;
                }
                else
                {
                    dictionaryOfArtists.Add(currentArtistName, 1);
                }
            }

            return dictionaryOfArtists;
        }
    }
}
