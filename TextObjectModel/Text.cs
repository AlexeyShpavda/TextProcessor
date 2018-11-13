using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Interfaces.TextObjectModel;

namespace TextObjectModel
{
    public class Text : IText
    {
        private ICollection<ISentence> Sentences { get; set; }

        public Text()
        {
            Sentences = new Collection<ISentence>();
        }

        public void Add(ISentence sentence)
        {
            Sentences.Add(sentence);
        }

        public ICollection<ISentence> GetSentences()
        {
            return Sentences;
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
    }
}
