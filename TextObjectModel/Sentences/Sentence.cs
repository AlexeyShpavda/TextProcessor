using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;
using TextObjectModel.SentenceElements;

namespace TextObjectModel.Sentences
{
    [DataContract(Namespace = "")]
    [KnownType(typeof(Word))]
    [KnownType(typeof(Separator))]
    public class Sentence : ISentence
    {
        private ICollection<ISentenceElement> _sentenceElements;

        [DataMember]
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
                if (value.Count == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(value));
                _sentenceElements = value;
            }
        }

        [DataMember]
        public ICollection<SentenceType> SentenceTypes { get; private set; }

        [DataMember]
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

        public void GetSentenceTypes()
        {
            ICollection<SentenceType> sentenceTypes = new Collection<SentenceType>();

            var lastSeparator = _sentenceElements.Last() as ISeparator;

            if (lastSeparator != null && lastSeparator.IsQuestionMark())
            {
                sentenceTypes.Add(SentenceType.InterrogativeSentence);
            }

            if (lastSeparator != null && lastSeparator.IsExclamationMark())
            {
                sentenceTypes.Add(SentenceType.ExclamatorySentence);
            }

            if (lastSeparator != null && sentenceTypes.Count == 0)
            {
                sentenceTypes.Add(SentenceType.DeclarativeSentence);
            }

            SentenceTypes = sentenceTypes;
        }

        public void GetTypeOfComplicatingStructures()
        {
            TypeOfComplicatingStructures = _sentenceElements.OfType<ISeparator>().Any(x => x.IsWordSeparationMark())
                ? TypeOfComplicatingStructures.ComplicatedSentence
                : TypeOfComplicatingStructures.UncomplicatedSentence;
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
    }
}
