using System.Collections.Generic;
using Interfaces.TextObjectModel.Sentences;

namespace Interfaces.TextObjectModel
{
    public interface IText
    {
        IList<ISentence> Sentences { get; }

        void SaveToXmlFile(string fileName);
    }
}
