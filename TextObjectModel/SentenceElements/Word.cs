using System;
using System.Linq;
using Interfaces.TextObjectModel.SentenceElements;

namespace TextObjectModel.SentenceElements
{
    public class Word : IWord
    {
        public string Chars { get; set; }

        public int Length => Chars.Length;

        public Word(string str)
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

        public override string ToString()
        {
            return Chars;
        }
    }
}
