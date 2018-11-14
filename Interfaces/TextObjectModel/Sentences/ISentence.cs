using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace Interfaces.TextObjectModel.Sentences
{
    public interface ISentence
    {
        ICollection<SentenceType> SentenceTypes { get; set; }

        TypeOfComplicatingStructures TypeOfComplicatingStructures { get; set; }

        void Add(ISentenceElement element);

        void Remove<T>(Predicate<T> predicate) where T : ISentenceElement;

        ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement;

        void SentenceUpdate(ICollection<ISentenceElement> sentenceElements);

        void ReplaceWord(Predicate<IWord> predicate, ICollection<ISentenceElement> sentenceElements);
    }
}
