using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    public class Word : IWord
    {
        public string Chars { get; set; }

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
