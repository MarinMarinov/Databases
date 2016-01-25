using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Task9AndTask10
{
    class Program
    {
        static void Main()
        {
            // Task 9 
            //Write a program to traverse given directory and write to a XML file its contents together with all subdirectories and files

            string directories = "../../../../";
            string filePath = "../../directoryInfoFirst.xml";


            XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);

            using (writer)
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 2;

                writer.WriteStartDocument();

                WriteDirectoryInfo(directories, writer);

                writer.WriteEndDocument();
            }

            // Task 10
            //Rewrite the last exercises using XDocument, XElement and XAttribute

            string secondFilePath = "../../directoryInfoSecond.xml";

            XElement dirX = new XElement("DirectoryInfo");
            XElement dirElements = GetDirectories(directories);
            dirX.Add(dirElements);
            dirX.Save(secondFilePath);


        }

        private static XElement GetDirectories(string directories)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directories);

            FileInfo[] files = directoryInfo.GetFiles();
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();

            XElement result = new XElement("Dir", new XAttribute("name", directoryInfo.Name));

            foreach (var file in files)
            {
                result.Add(new XElement("File", new XAttribute("Name", file.Name)));
            }

            foreach(var dir in subDirectories)
            {
                result.Add(GetDirectories(dir.FullName));
            }

            return result;
        }

        private static void WriteDirectoryInfo(string directories, XmlWriter writer)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directories);

            FileInfo[] files = directoryInfo.GetFiles();
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();

            writer.WriteStartElement("Dir");
            writer.WriteStartAttribute("name");
            writer.WriteValue(directoryInfo.Name);
            writer.WriteEndAttribute();

            foreach (var file in files)
            {
                writer.WriteStartElement("File");
                writer.WriteStartAttribute("name");
                writer.WriteValue(file.Name);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }

            foreach (var dir in subDirectories)
            {
                WriteDirectoryInfo(dir.FullName, writer);
            }

            writer.WriteEndElement();
        }
    }
}
