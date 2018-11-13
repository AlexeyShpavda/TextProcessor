using System.IO;
using Interfaces.TextObjectModel;

namespace Interfaces.SyntacticalAnalyzer
{
    public interface ITextParser
    {
        IText Parse(StreamReader streamReader);
    }
}
