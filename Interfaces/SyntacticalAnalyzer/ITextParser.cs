using System.IO;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.Sentences;

namespace Interfaces.SyntacticalAnalyzer
{
    public interface ITextParser
    {
        IText Parse(StreamReader streamReader);

        ISentence Parse(string inputLine);
    }
}
