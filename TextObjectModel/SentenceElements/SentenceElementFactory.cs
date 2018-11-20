using System.Collections.Generic;
using System.Linq;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    public class SentenceElementFactory
    {
        private readonly IDictionary<string, ISentenceElement> _sentenceElements =
            new Dictionary<string, ISentenceElement>();

        public ISentenceElement GetCharacter(string key)
        {
            ISentenceElement sentenceElement;
            if (_sentenceElements.ContainsKey(key))
            {
                sentenceElement = _sentenceElements[key];
            }
            else
            {
                if (SeparatorConstants.AllSentenceSeparators.Contains(key))
                {
                    sentenceElement = new Separator(key);
                }
                else
                {
                    sentenceElement = new Word(key);
                }
                _sentenceElements.Add(key, sentenceElement);
            }

            return sentenceElement;
        }
    }
}
