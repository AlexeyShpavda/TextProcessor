using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Interfaces.TextObjectModel;

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
