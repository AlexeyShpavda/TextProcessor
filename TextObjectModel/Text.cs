using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;

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

        public ICollection<ISentence> GetSentences(Func<ISentence, bool> selector = null)
        {
            return selector == null ? Sentences : Sentences.Where(selector).ToList();
        }

        public ICollection<ISentence> SortSentencesAscending<T>() where T : ISentenceElement
        {
            return Sentences.OrderBy(x => x.GetElements<T>().Count).ToList();
        }

        public ICollection<ISentence> SortSentencesDescending<T>() where T : ISentenceElement
        {
            return Sentences.OrderByDescending(x => x.GetElements<T>().Count).ToList();
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
