using System.Linq;
using System.Text.RegularExpressions;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    public class Separator : ISeparator
    {
        public static string[] SentenceSeparators { get; } = {"...", "!", ".", "?", "?!", "!?"};

        public static string[] WordSeparators { get; } = {",", ";", ":"};

        public string Chars { get; set; }

        public Separator(string str)
        {
            Chars = str;
        }

        public bool IsWordSeparator()
        {
            return WordSeparators.Any(x => Chars.Contains(x));
        }

        public bool IsExclamationMark()
        {
            return Chars.Contains('!');
        }

        public bool IsQuestionMark()
        {
            return Chars.Contains('?');
        }

        public bool IsDeclarativeMark()
        {
            return Chars.Contains('.');
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
