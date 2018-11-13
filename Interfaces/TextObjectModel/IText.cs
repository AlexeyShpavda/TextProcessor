using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;

namespace Interfaces.TextObjectModel
{
    public interface IText
    {
        void Add(ISentence sentence);

        ICollection<ISentence> GetSentences();

        ICollection<ISentence> SortSentencesAscending<T>() where T : ISentenceElement;

        ICollection<ISentence> SortSentencesDescending<T>() where T : ISentenceElement;
    }
}
