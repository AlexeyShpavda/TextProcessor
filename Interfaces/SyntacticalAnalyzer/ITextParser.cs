using System.Collections.Generic;
using System.IO;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;

namespace Interfaces.SyntacticalAnalyzer
{
    public interface ITextParser
    {
        IText Parse(StreamReader streamReader);

        ICollection<ISentenceElement> Parse(string inputLine);
    }
}
