using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    public class Separator : ISeparator
    {
        public static string[] EndPunctuationSeparators { get; } = {"... ", "! ", ". ", "? ", "?! ", "!? "};

        private string _chars;

        public string Chars
        {
            get => _chars;
            set => _chars = Regex.Replace(value, "[\f\n\r\t\v]", " ");
        }

        public Separator(string str)
        {
            Chars = str;
        }

        public static bool IsEndPunctuationSeparator(string str)
        {
            return EndPunctuationSeparators.Any(separator => separator == str);
        }

        public static bool IsExclamationMark(string str)
        {
            return str.Contains('!');
        }

        public static bool IsQuestionMark(string str)
        {
            return str.Contains('?');
        }

        public static bool IsDeclarativeSentence(string str)
        {
            return str.Contains('.');
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
