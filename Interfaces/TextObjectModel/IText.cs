using System.Collections.Generic;

namespace Interfaces.TextObjectModel
{
    public interface IText
    {
        void Add(ISentence sentence);

        ICollection<ISentence> GetSentences();
    }
}
