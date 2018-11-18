using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;
using TextObjectModel.Sentences;

namespace TextObjectModel
{
    public class TextFormatting
    {
        public ICollection<ISentence> SelectSentences(IText text, Func<ISentence, bool> selector = null)
        {
            return selector == null ? text.Sentences : text.Sentences.Where(selector).ToList();
        }

        public IOrderedEnumerable<ISentence> SortSentencesAscending<T>(IText text) where T : ISentenceElement
        {
            return text.Sentences.OrderBy(x => SelectElements<T>(x).Count);
        }

        public IOrderedEnumerable<ISentence> SortSentencesDescending<T>(IText text) where T : ISentenceElement
        {
            return text.Sentences.OrderByDescending(x => SelectElements<T>(x).Count);
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

            var sentences = new List<ISentence>();
            var elementsForNewSentences = new List<ISentenceElement>();

            elementsForNewSentences.AddRange(ReplaceWord(text.Sentences[sentenceIndex],
                x => x.Length == wordLength, sentenceElements));

            var elementsForOneNewSentence = new List<ISentenceElement>();

            foreach (var sentenceElement in elementsForNewSentences)
            {
                if (sentenceElement is ISeparator separator && separator.IsSentenceSeparationMark())
                {
                    sentences.Add(new Sentence(elementsForOneNewSentence.ToList()));
                    elementsForOneNewSentence.Clear();
                }
                else
                {
                    elementsForOneNewSentence.Add(sentenceElement);
                }
            }

            if (elementsForOneNewSentence.Count <= 0) return;

            var nextSentenceIndex = sentenceIndex + 1;
            elementsForOneNewSentence.AddRange(text.Sentences[nextSentenceIndex].SentenceElements);
            sentences.Add(new Sentence(elementsForOneNewSentence.ToList()));

            text.Sentences.RemoveAt(sentenceIndex);
            text.Sentences.RemoveAt(nextSentenceIndex);

            AddSentencesToTextByIndex(text, sentenceIndex, sentences);
        }

        public ICollection<ISentenceElement> ReplaceWord(ISentence sentence, Predicate<IWord> predicate,
            ICollection<ISentenceElement> sentenceElements)
        {
            var newSentenceElements = new List<ISentenceElement>();

            var needToAddElement = true;
            foreach (var element in sentence.SentenceElements)
            {
                if (element is IWord word && predicate(word))
                {
                    if (((ISeparator) sentenceElements.Last()).IsSpaceMark())
                    {
                        newSentenceElements.AddRange(sentenceElements.Where(x => x != sentenceElements.Last()));
                    }
                    else
                    {
                        newSentenceElements.AddRange(sentenceElements);
                        needToAddElement = false;
                    }
                }
                else
                {
                    if (needToAddElement)
                    {
                        newSentenceElements.Add(element);
                    }
                    else
                    {
                        needToAddElement = true;
                    }
                }
            }

            return newSentenceElements.ToList();
        }

        public void AddSentencesToTextByIndex(IText text, int sentenceIndex, ICollection<ISentence> sentences)
        {
            var newTextSentences = text.Sentences.ToList();
            newTextSentences.InsertRange(sentenceIndex, sentences);
            text.Sentences.Clear();
            foreach (var sentence in newTextSentences)
            {
                text.Sentences.Add(sentence);
            }
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
