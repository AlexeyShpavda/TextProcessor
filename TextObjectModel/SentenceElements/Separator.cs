using System.Linq;
using System.Runtime.Serialization;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    [DataContract(Namespace = "")]
    public class Separator : SentenceElement, ISeparator
    {
        public Separator(string str) : base(str)
        {
            Chars = str;
        }

        public bool IsSpaceMark()
        {
            return Chars.Equals(SeparatorConstants.Space);
        }

        public bool IsWordSeparationMark()
        {
            return SeparatorConstants.WordSeparationMarks.Any(x => Chars.Equals(x));
        }

        public bool IsSentenceSeparationMark()
        {
            return SeparatorConstants.SentenceSeparationMarks.Any(x => Chars.Equals(x));
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
