using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Task13AndTask14
{
    class Program
    {
        static void Main(string[] args)
        {

            XPathDocument xPathDoc = new XPathDocument("../../../Tasks1-Task6AndTask8/catalogue.xml");
            XslTransform xslTransformer = new XslTransform();
            xslTransformer.Load("../../Catalogue.xslt");

            XmlTextWriter writer = new XmlTextWriter("../../catalogue.html", Encoding.UTF8);
            xslTransformer.Transform(xPathDoc, null, writer);
        }
    }
}
