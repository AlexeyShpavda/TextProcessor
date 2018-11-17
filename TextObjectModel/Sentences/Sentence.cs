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
    public class Sentence : ISentence//, IXmlSerializable
    {
        private ICollection<ISentenceElement> _sentenceElements;

        public ICollection<ISentenceElement> SentenceElements
        {
            get
            {
                GetSentenceTypes();

                GetTypeOfComplicatingStructures();

                return _sentenceElements;
            }

            private set
            {
                SentenceTypes.Clear();

                if (value.Count == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(value));
                _sentenceElements = value;
            }
        }

        public ICollection<SentenceType> SentenceTypes { get; private set; }

        public TypeOfComplicatingStructures TypeOfComplicatingStructures { get; private set; }

        public Sentence()
        {
            _sentenceElements = new Collection<ISentenceElement>();
            SentenceTypes = new Collection<SentenceType>();
        }

        public Sentence(ICollection<ISentenceElement> sentenceElements) : this()
        {
            SentenceElements = sentenceElements;
        }

        public ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement
        {
            return selector == null
                ? SentenceElements.OfType<T>().ToList()
                : SentenceElements.OfType<T>().Where(selector).ToList();
        }

        public void GetSentenceTypes()
        {
            SentenceTypes.Clear();

            var lastSeparator = _sentenceElements.Last() as ISeparator;

            if (lastSeparator != null && lastSeparator.IsQuestionMark())
            {
                SentenceTypes.Add(SentenceType.InterrogativeSentence);
            }

            if (lastSeparator != null && lastSeparator.IsDeclarativeMark())
            {
                SentenceTypes.Add(SentenceType.DeclarativeSentence);
            }

            if (lastSeparator != null && lastSeparator.IsExclamationMark())
            {
                SentenceTypes.Add(SentenceType.ExclamatorySentence);
            }
        }

        public void GetTypeOfComplicatingStructures()
        {
            TypeOfComplicatingStructures = _sentenceElements.OfType<ISeparator>().Any(x => x.IsWordSeparator())
                ? TypeOfComplicatingStructures.ComplicatedSentence
                : TypeOfComplicatingStructures.UncomplicatedSentence;
        }

        public void SentenceUpdate(ICollection<ISentenceElement> sentenceElements)
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

        //public XmlSchema GetSchema()
        //{
        //    return null;
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void WriteXml(XmlWriter writer)
        //{
        //    var sentenceTypesSerializer = new XmlSerializer(typeof(List<SentenceType>));

        //    sentenceTypesSerializer.Serialize(writer, SentenceTypes.ToList());

        //    var typeOfComplicatingStructuresSerializer = new XmlSerializer(typeof(TypeOfComplicatingStructures));

        //    typeOfComplicatingStructuresSerializer.Serialize(writer, TypeOfComplicatingStructures);


        //    var wordSerializer = new XmlSerializer(typeof(Word));
        //    var separatorSerializer = new XmlSerializer(typeof(Separator));

        //    foreach (var element in SentenceElements)
        //    {
        //        if (element is Word word)
        //        {
        //            wordSerializer.Serialize(writer, word);
        //        }
        //        else
        //        {
        //            separatorSerializer.Serialize(writer, element);
        //        }
        //    }
        //}
    }
}
