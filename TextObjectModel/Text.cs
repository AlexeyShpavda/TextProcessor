using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.Sentences;
using TextObjectModel.Sentences;

namespace TextObjectModel
{
    public class Text : IText, IXmlSerializable
    {
        public IList<ISentence> Sentences { get; set; }

        public Text()
        {
            Sentences = new List<ISentence>();
        }

        public Text(IList<ISentence> sentences)
        {
            Sentences = sentences;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in Sentences)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }

        public void SaveToXmlFile(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(Text));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, this);
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            var sentenceSerializer = new XmlSerializer(typeof(List<Sentence>));

            sentenceSerializer.Serialize(writer, Sentences.OfType<Sentence>().ToList());
        }
    }
}
