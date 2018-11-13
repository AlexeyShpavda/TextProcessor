using System.Linq;
using System.Text.RegularExpressions;
using Interfaces.TextObjectModel;

namespace TextObjectModel.SentenceElements
{
    public class Separator : ISentenceElement
    {
        public static string[] EndPunctuationSeparators { get; } = {"... ", "! ", ". ", "? ", "?! ", "!? "};

        private string _chars;

        public string Chars
        {
            get => _chars;
            private set => _chars = Regex.Replace(value, "[\f\n\r\t\v]", " ");
        }

        public Separator(string str)
        {
            Chars = str;
        }

        public static bool IsEndPunctuationSeparator(string str)
        {
            return EndPunctuationSeparators.Any(separator => separator == str);
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
