using System.IO;
using System.Text.RegularExpressions;
using Interfaces.SyntacticalAnalyzer;
using Interfaces.TextObjectModel;
using TextObjectModel;
using TextObjectModel.SentenceElements;
using TextObjectModel.Sentences;

namespace SyntacticalAnalyzer
{
    public class TextParser : ITextParser
    {
        public IText Parse(StreamReader streamReader)
        {
            const string pattern = @"\b(\w+)((\p{P}{0,3})\s?)";

            var text = new Text();
            var sentence = new Sentence();

            string fileLine;
            while ((fileLine = streamReader.ReadLine()) != null)
            {
                fileLine = string.Concat(fileLine, " ");
                foreach (Match match in Regex.Matches(fileLine, pattern))
                {
                    sentence.Add(new Word(match.Groups[1].ToString()));

                    sentence.Add(new Separator(match.Groups[2].ToString()));

                    if (Separator.IsEndPunctuationSeparator(match.Groups[2].ToString()))
                    {
                        text.Add(sentence);
                        sentence = new Sentence();
                    }
                }
            }

            return text;
        }
    }
}
