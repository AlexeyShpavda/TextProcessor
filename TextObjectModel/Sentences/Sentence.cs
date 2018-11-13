using System.Collections.Generic;
using Interfaces.TextObjectModel;

namespace TextObjectModel.Sentences
{
    public class Sentence : ISentence
    {
        private ICollection<ISentenceElement> SentenceElements { get; set; }

        public void Add(ISentenceElement element)
        {
            SentenceElements.Add(element);
        }
    }
}
