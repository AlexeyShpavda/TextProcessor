using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
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

        public IText DeleteWordsStartingWithConsonant(IText text, int wordLength)
        {
            var newSentences = text.Sentences
                .Select(x => RemoveWordsFromSentence(x, y => y.Length == wordLength && y.StartWithConsonant()))
                .Where(x => x.SentenceElements.OfType<IWord>().Any() && x.SentenceElements.Count > 0).ToList();

            return new Text(newSentences);
        }

        public IText ReplacesWordsInSentenceWithSubstring(IText text, int sentenceNumber, int wordLength,
            ICollection<ISentenceElement> sentenceElements)
        {
            var sentenceIndex = sentenceNumber - 1;

            var sentencesForNewText = new List<ISentence>();
            var elementsForNewSentences = new List<ISentenceElement>();
            var elementsForOneNewSentence = new List<ISentenceElement>();

            elementsForNewSentences.AddRange(ReplaceWord(text.Sentences[sentenceIndex],
                x => x.Length == wordLength, sentenceElements));

            // If it is true, then no words were found for deletion.
            if (text.Sentences[sentenceIndex].SentenceElements.Count != elementsForNewSentences.Count)
            {
                foreach (var sentenceElement in elementsForNewSentences)
                {
                    elementsForOneNewSentence.Add(sentenceElement);

                    if (!(sentenceElement is ISeparator separator) || !separator.IsSentenceSeparationMark()) continue;

                    sentencesForNewText.Add(new Sentence(elementsForOneNewSentence.ToList()));

                    elementsForOneNewSentence.Clear();
                }
            }

            if (elementsForOneNewSentence.Count == 0)
            {
                return new Text(AddSentencesToTextByIndex(text, sentenceIndex, sentencesForNewText));
            }

            var nextSentenceIndex = sentenceIndex + 1;

            elementsForOneNewSentence.AddRange(text.Sentences[nextSentenceIndex].SentenceElements);

            sentencesForNewText.Add(new Sentence(elementsForOneNewSentence.ToList()));

            text.Sentences.RemoveAt(sentenceIndex);
            text.Sentences.RemoveAt(sentenceIndex);

            return new Text(AddSentencesToTextByIndex(text, sentenceIndex, sentencesForNewText));
        }

        public ICollection<ISentenceElement> ReplaceWord(ISentence sentence, Predicate<IWord> predicate,
            ICollection<ISentenceElement> sentenceElements)
        {
            var newSentenceElements = sentence.SentenceElements.ToList();
            var matchingWords = GetMatchingElements(newSentenceElements, predicate);
            if (matchingWords.Any())
            {
                foreach (var element in matchingWords)
                {
                    var index = newSentenceElements.IndexOf(element);

                    newSentenceElements.Remove(element);

                    newSentenceElements.RemoveAt(index);

                    newSentenceElements.InsertRange(index, sentenceElements);
                }
            }

            return newSentenceElements.Count != 0 ? new List<ISentenceElement>(newSentenceElements) : null;
        }

        public IList<ISentence> AddSentencesToTextByIndex(IText text, int sentenceIndex,
            ICollection<ISentence> sentences)
        {
            var newTextSentences = text.Sentences.ToList();

            newTextSentences.InsertRange(sentenceIndex, sentences);

            return new List<ISentence>(newTextSentences);
        }

        public ISentence RemoveWordsFromSentence(ISentence sentence, Predicate<IWord> predicate)
        {
            var newSentenceElements = sentence.SentenceElements.ToList();
            var matchingWords = GetMatchingElements(newSentenceElements, predicate);
            if (matchingWords.Any())
            {
                foreach (var element in matchingWords)
                {
                    var index = newSentenceElements.IndexOf(element);

                    if (index == newSentenceElements.Count - 2 && index > 0) index--;

                    newSentenceElements.Remove(element);

                    if (newSentenceElements.Count > 1) newSentenceElements.RemoveAt(index);
                }
            }

            return new Sentence(newSentenceElements);
        }

        public ICollection<T> SelectElements<T>(ISentence sentence, Func<T, bool> selector = null)
            where T : ISentenceElement
        {
            return selector == null
                ? sentence.SentenceElements.OfType<T>().ToList()
                : sentence.SentenceElements.OfType<T>().Where(selector).ToList();
        }

        public IList<T>GetMatchingElements<T>(IList<ISentenceElement> sentenceElements, Predicate<T> predicate)
        {
            return sentenceElements.OfType<T>().ToList().FindAll(predicate);
        }

        public void SaveToXmlFile(IText text,string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                using (var xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings { Indent = true }))
                {
                    var serializer = new DataContractSerializer(typeof(Text));
                    serializer.WriteObject(xmlWriter, text);
                }
            }
        }

        public static Text ReadFromXmlFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (var xmlDictionaryReader =
                    XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    var serializer = new DataContractSerializer(typeof(Text));
                    return (Text)serializer.ReadObject(xmlDictionaryReader);
                }
            }
        }
    }
}
