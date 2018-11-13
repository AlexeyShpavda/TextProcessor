using System;
using System.Collections.Generic;
using Interfaces.TextObjectModel.SentenceElements;

namespace Interfaces.TextObjectModel.Sentences
{
    public interface ISentence
    {
        void Add(ISentenceElement element);

        ICollection<T> GetElements<T>(Func<T, bool> selector = null) where T : ISentenceElement;
    }
}
