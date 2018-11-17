using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace TextObjectModel
{
    public class TextFormatting
    {
        public ICollection<ISentence> SelectSentences(IText text, Func<ISentence, bool> selector = null)
        {
            return selector == null ? text.Sentences : text.Sentences.Where(selector).ToList();
        }

        public ICollection<ISentence> SortSentencesAscending<T>(IText text) where T : ISentenceElement
        {
            return text.Sentences.OrderBy(x => x.SentenceElements.Count).ToList();
        }

        public ICollection<ISentence> SortSentencesDescending<T>(IText text) where T : ISentenceElement
        {
            return text.Sentences.OrderByDescending(x => x.SentenceElements.Count).ToList();
        }

        public IEnumerable<IWord> GetWordsFromSentences(IText text, SentenceType sentenceType, int wordLength)
        {
            return SelectSentences(text, x => x.SentenceTypes.Contains(sentenceType))
                .SelectMany(y => SelectElements<IWord>(y, z => z.Length == wordLength)).Distinct();
        }

        public void DeleteWordsStartingWithConsonant(IText text, int wordLength)
        {
            foreach (var sentence in text.Sentences)
            {
                RemoveFromSentence<IWord>(sentence, x => x.Length == wordLength && x.StartWithConsonant());
            }
        }

        public void ReplacesWordsInSentenceWithSubstring(IText text, int sentenceNumber, int wordLength,
            ICollection<ISentenceElement> sentenceElements)
        {
            var sentenceIndex = sentenceNumber - 1;
            ReplaceWord(text.Sentences[sentenceIndex], x => x.Length == wordLength, sentenceElements);
        }

        public void ReplaceWord(ISentence sentence, Predicate<IWord> predicate,
            ICollection<ISentenceElement> sentenceElements)
        {
            var newSentenceElements = new List<ISentenceElement>();

            foreach (var element in sentence.SentenceElements)
            {
                if (element is IWord word && predicate(word))
                {
                    newSentenceElements.AddRange(sentenceElements);
                    continue;
                }

                newSentenceElements.Add(element);
            }

            sentence.SentenceUpdate(newSentenceElements);
        }

        public void RemoveFromSentence<T>(ISentence sentence, Predicate<T> predicate) where T : ISentenceElement
        {
            sentence.SentenceUpdate(sentence.SentenceElements.Where(x => !(x is T t && predicate(t))).ToList());
        }

        public ICollection<T> SelectElements<T>(ISentence sentence, Func<T, bool> selector = null) where T : ISentenceElement
        {
            return selector == null
                ? sentence.SentenceElements.OfType<T>().ToList()
                : sentence.SentenceElements.OfType<T>().Where(selector).ToList();
        }
    }
}
