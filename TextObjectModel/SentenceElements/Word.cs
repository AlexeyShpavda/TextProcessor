using Interfaces.TextObjectModel;

namespace TextObjectModel.SentenceElements
{
    public class Word : ISentenceElement
    {
        public string Chars { get; private set; }

        public Word(string str)
        {
            Chars = str;
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
