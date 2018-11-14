using System.IO;
using System.Text.RegularExpressions;
using Interfaces.SyntacticalAnalyzer;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.Sentences;
using Interfaces.TextObjectModel.Sentences.Enums;
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

                    if (!Separator.IsEndPunctuationSeparator(match.Groups[2].ToString())) continue;

                    if (Separator.IsQuestionMark(match.Groups[2].ToString()))
                    {
                        sentence.SentenceTypes.Add(SentenceType.InterrogativeSentence);
                    }


                    if (Separator.IsDeclarativeSentence(match.Groups[2].ToString()))
                    {
                        sentence.SentenceTypes.Add(SentenceType.DeclarativeSentence);
                    }


                    if (Separator.IsExclamationMark(match.Groups[2].ToString()))
                    {
                        sentence.SentenceTypes.Add(SentenceType.ExclamatorySentence);
                    }

                    text.Add(sentence);
                    sentence = new Sentence();
                }
            }

            return text;
        }

        public ISentence Parse(string inputLine)
        {
            const string pattern = @"\b(\w+)((\p{P}{0,3})\s?)";

            var sentence = new Sentence();

            var line = inputLine;

            line = string.Concat(line, " ");
            foreach (Match match in Regex.Matches(line, pattern))
            {
                sentence.Add(new Word(match.Groups[1].ToString()));

                sentence.Add(new Separator(match.Groups[2].ToString()));
            }

            return sentence;
        }
    }
}
