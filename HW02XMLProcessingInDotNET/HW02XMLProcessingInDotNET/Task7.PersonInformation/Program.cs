//In a text file we are given the name, address and phone number of given person (each at a single line).
//Write a program, which creates new XML document, which contains these data in structured XML format.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task7.PersonInformation
{
    class Program
    {
        static void Main()
        {
            // Creating person.txt
            string textFilePath = "../../person.txt";
            StreamWriter writer = new StreamWriter(textFilePath);

            using (writer)
            {
                writer.WriteLine("Иван Иванов");
                writer.WriteLine("Перущица, ул.'Освобождение', №1");
                writer.WriteLine("+359888123456");
            }

            StreamReader reader = new StreamReader(textFilePath);

            using (reader)
            {
                var person = new
                {
                    Name = reader.ReadLine(),
                    Address = reader.ReadLine(),
                    Phone = reader.ReadLine()
                };

                XElement personXElement = new XElement("Person",
                    new XElement("Name", person.Name),
                    new XElement("Address", person.Address),
                    new XElement("Phone", person.Phone));

                personXElement.Save("../../person.xml");
            }
        }
    }
}
