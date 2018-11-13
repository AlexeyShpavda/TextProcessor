using Interfaces.TextObjectModel;

namespace TextObjectModel.SentenceElements
{
    public abstract class SentenceElement : IToken
    {
        public string Chars { get; protected set; }
    }
}
