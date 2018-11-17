using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace Interfaces.TextObjectModel.Sentences
{
    public interface ISentence
    {
        ICollection<ISentenceElement> SentenceElements { get; }

        ICollection<SentenceType> SentenceTypes { get; }

        TypeOfComplicatingStructures TypeOfComplicatingStructures { get; }

        ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement;

        void SentenceUpdate(ICollection<ISentenceElement> sentenceElements);
    }
}
