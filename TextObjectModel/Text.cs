using System.Collections.Generic;
using Interfaces.TextObjectModel;

namespace TextObjectModel
{
    public class Text : IText
    {
        private ICollection<ISentence> Sentences { get; set; }

        public void Add(ISentence sentence)
        {
            Sentences.Add(sentence);
        }
    }
}
