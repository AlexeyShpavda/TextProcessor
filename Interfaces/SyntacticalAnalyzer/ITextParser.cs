using System.IO;
using Interfaces.TextObjectModel;

namespace Interfaces.SyntacticalAnalyzer
{
    public interface ITextParser
    {
        void Parse(StreamReader sr, out IText text);
    }
}
