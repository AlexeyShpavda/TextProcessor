using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;

namespace Interfaces.TextObjectModel.Sentences
{
    public interface ISentence
    {
        ICollection<ISentenceElement> SentenceElements { get; set; }

        ICollection<SentenceType> SentenceTypes { get; set; }

        TypeOfComplicatingStructures TypeOfComplicatingStructures { get; set; }
    }
}
