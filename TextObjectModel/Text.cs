using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.Sentences;
using TextObjectModel.Sentences;

namespace TextObjectModel
{
    [DataContract(Namespace = "")]
    [KnownType(typeof(Sentence))]
    public class Text : IText
    {
        private IList<ISentence> _sentences;

        [DataMember]
        public IList<ISentence> Sentences
        {
            get
            {
                //_sentences = (from sentence in _sentences
                //              where sentence.SentenceElements.Count != 0
                //              select sentence).ToList();

                return _sentences;
            }
            private set
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
                using (var xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings {Indent = true}))
                {
                    var serializer = new DataContractSerializer(typeof(Text));
                    serializer.WriteObject(xmlWriter, this);
                }
            }
        }

        public static Text ReadFromXmlFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (var xmlDictionaryReader =
                    XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    var serializer = new DataContractSerializer(typeof(Text));
                    return (Text) serializer.ReadObject(xmlDictionaryReader);
                }
            }
        }
    }
}
