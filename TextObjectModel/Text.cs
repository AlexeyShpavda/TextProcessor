using System;
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
        private IList<ISentence> _sentences;

        public IList<ISentence> Sentences
        {
            get
            {
                _sentences = (from sentence in _sentences
                              where sentence.SentenceElements.Count != 0
                              select sentence).ToList();

                return _sentences;
            }
            set
            {
                if (value.Count == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(value));

                _sentences = value;
            }
        }

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
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(Text));
                xmlSerializer.Serialize(fileStream, this);
            }
        }

        public static Text ReadFromXmlFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(typeof(Text));
                return (Text) xmlSerializer.Deserialize(fileStream);
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
