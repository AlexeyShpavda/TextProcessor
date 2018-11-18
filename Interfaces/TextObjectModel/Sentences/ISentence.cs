using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace Interfaces.TextObjectModel.Sentences
{
    public interface ISentence
    {
        ICollection<ISentenceElement> SentenceElements{ get; }

        ICollection<SentenceType> SentenceTypes { get; }

        TypeOfComplicatingStructures TypeOfComplicatingStructures { get; }

        void SentenceUpdate(ICollection<ISentenceElement> sentenceElements);
    }
}
