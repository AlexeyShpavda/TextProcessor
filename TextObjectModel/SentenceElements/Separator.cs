using System.Linq;
using System.Runtime.Serialization;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    [DataContract(Namespace = "")]
    public class Separator : SentenceElement, ISeparator
    {
        public static string Space { get; } = " ";

        public static string[] WordSeparationMarks { get; } = {", ", "; ", ": "};

        public static string[] SentenceSeparationMarks { get; } = {"... ", "! ", ". ", "? ", "?! ", "!? "};

        public Separator()
        {
            Chars = string.Empty;
        }

        public Separator(string str)
        {
            Chars = str;
        }

        public bool IsSpaceMark()
        {
            return Chars.Equals(Space);
        }

        public bool IsWordSeparationMark()
        {
            return WordSeparationMarks.Any(x => Chars.Equals(x));
        }

        public bool IsSentenceSeparationMark()
        {
            return SentenceSeparationMarks.Any(x => Chars.Equals(x));
        }

        public bool IsExclamationMark()
        {
            return Chars.Contains('!');
        }

        public bool IsQuestionMark()
        {
            return Chars.Contains('?');
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
