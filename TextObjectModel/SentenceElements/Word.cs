using System;
using System.Linq;
using System.Runtime.Serialization;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    [DataContract(Namespace = "")]
    public class Word : SentenceElement, IWord
    {
        public int Length => Chars.Length;

        public Word(string str) : base(str)
        {
            Chars = str;
        }

        public bool StartWithVowel()
        {
            var vowels = new[] {'a', 'e', 'i', 'o', 'u', 'y'};
            return vowels.Any(vowel => vowel == Chars.ToLower().First());
        }

        public bool StartWithConsonant()
        {
            return !StartWithVowel();
        }

        public static bool operator ==(Word word1, Word word2)
        {
            return string.Equals(word1.ToString(), word2.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool operator !=(Word word1, Word word2)
        {
            return !(word1 == word2);
        }

        public override bool Equals(object obj)
        {
            return this == (Word) obj;
        }

        public override int GetHashCode()
        {
            return ToString().ToLower().GetHashCode();
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
