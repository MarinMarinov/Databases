using System;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Task16
{
    class Program
    {
        static void Main()
        {
            // catalogue.xsd is generated from the catalogue.xsl file of the "Task1-Task6AndTask8" project

            var schema = new XmlSchemaSet();
            schema.Add(string.Empty, "../../catalogue.xsd");

            XDocument xDocValid = XDocument.Load("../../../Tasks1-Task6AndTask8/catalogue.xml");
            XDocument xDocInvalid = XDocument.Load("../../invalidCatalogue.xml");

            PrintValidationResult(xDocValid, schema);
            PrintValidationResult(xDocInvalid, schema);
        }

        private static void PrintValidationResult(XDocument doc, XmlSchemaSet schema)
        {
            var errorOccured = false;
            doc.Validate(schema, (obj, ev) =>
            {
                Console.WriteLine(ev.Message);
                errorOccured = true;
            });

            if (!errorOccured)
            {
                Console.WriteLine("No Error occured!");
            }
        }
    }
}
