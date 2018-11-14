using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace Interfaces.TextObjectModel
{
    public interface IText
    {
        void Add(ISentence sentence);

        ICollection<ISentence> GetSentences(Func<ISentence, bool> selector = null);

        ICollection<ISentence> SortSentencesAscending<T>() where T : ISentenceElement;

        ICollection<ISentence> SortSentencesDescending<T>() where T : ISentenceElement;

        IEnumerable<IWord> GetWordsFromSentences(SentenceType sentenceType, int wordLength);

        void DeleteWordsStartingWithConsonant(int wordLength);
    }
}
