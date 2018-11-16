using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;
using TextObjectModel.SentenceElements;

namespace TextObjectModel.Sentences
{
    public class Sentence : ISentence, IXmlSerializable
    {
        private ICollection<ISentenceElement> _sentenceElements;

        public ICollection<ISentenceElement> SentenceElements
        {
            get => _sentenceElements;

            set
            {
                _sentenceElements = value;

                var lastSeparator = SentenceElements.Last() as ISeparator;

                if (lastSeparator != null && lastSeparator.IsQuestionMark())
                {
                    SentenceTypes.Add(SentenceType.InterrogativeSentence);
                }

                if (lastSeparator != null && lastSeparator.IsDeclarativeMark())
                {
                    SentenceTypes.Add(SentenceType.DeclarativeSentence);
                }

                if (lastSeparator != null && lastSeparator.IsDeclarativeMark())
                {
                    SentenceTypes.Add(SentenceType.ExclamatorySentence);
                }

                TypeOfComplicatingStructures = SentenceElements.OfType<ISeparator>().Any(x => x.IsWordSeparator())
                    ? TypeOfComplicatingStructures.ComplicatedSentence
                    : TypeOfComplicatingStructures.UncomplicatedSentence;
            }
        }

        public ICollection<SentenceType> SentenceTypes { get; set; }

        public TypeOfComplicatingStructures TypeOfComplicatingStructures { get; set; }

        public Sentence()
        {
            _sentenceElements = new Collection<ISentenceElement>();
            SentenceTypes = new Collection<SentenceType>();
        }

        public Sentence(ICollection<ISentenceElement> sentenceElements) : this()
        {
            SentenceElements = sentenceElements;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var element in SentenceElements)
            {
                stringBuilder.Append(element);
            }

            return stringBuilder.ToString();
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
            var keySerializer = new XmlSerializer(typeof(Word));
            var keySerializer2 = new XmlSerializer(typeof(Separator));

            foreach (var element in SentenceElements)
            {
                if (element is Word word)
                {
                    keySerializer.Serialize(writer, word);
                }
                else
                {
                    keySerializer2.Serialize(writer, element);
                }
            }
        }
    }
}
