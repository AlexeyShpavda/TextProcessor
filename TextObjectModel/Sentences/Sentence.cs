using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;

namespace TextObjectModel.Sentences
{
    public class Sentence : ISentence
    {
        private ICollection<ISentenceElement> SentenceElements { get; set; }

        public Sentence()
        {
            SentenceElements = new Collection<ISentenceElement>();
        }

        public void Add(ISentenceElement element)
        {
            SentenceElements.Add(element);
        }

        public ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement
        {
            return selector == null
                ? SentenceElements.OfType<T>().ToList()
                : SentenceElements.OfType<T>().Where(selector).ToList();
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
