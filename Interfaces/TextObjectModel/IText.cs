using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;

namespace Interfaces.TextObjectModel
{
    public interface IText
    {
        void Add(ISentence sentence);

        ICollection<ISentence> GetSentences(Func<ISentence, bool> selector = null);

        ICollection<ISentence> SortSentencesAscending<T>() where T : ISentenceElement;

        ICollection<ISentence> SortSentencesDescending<T>() where T : ISentenceElement;
    }
}
