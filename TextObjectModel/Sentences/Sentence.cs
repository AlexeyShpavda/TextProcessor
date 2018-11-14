using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace TextObjectModel.Sentences
{
    public class Sentence : ISentence
    {
        private ICollection<ISentenceElement> SentenceElements { get; set; }

        public ICollection<SentenceType> SentenceTypes { get; set; }

        public Sentence()
        {
            SentenceElements = new Collection<ISentenceElement>();
            SentenceTypes = new Collection<SentenceType>();
        }

        public void Add(ISentenceElement element)
        {
            SentenceElements.Add(element);
        }

        public void Remove<T>(Predicate<T> predicate) where T : ISentenceElement
        {
            SentenceElements = SentenceElements.Where(x => !(x is T t && predicate(t))).ToList();
        }

        public ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement
        {
            return selector == null
                ? SentenceElements.OfType<T>().ToList()
                : SentenceElements.OfType<T>().Where(selector).ToList();
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
    }
}
