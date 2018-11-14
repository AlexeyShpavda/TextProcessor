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
        private const string Pattern = @"\b(\w+)((\p{P}{0,3})\s?)";

        public IText Parse(StreamReader streamReader)
        {
            var text = new Text();
            var sentence = new Sentence();

            try
            {
                string fileLine;
                while ((fileLine = streamReader.ReadLine()) != null)
                {
                    fileLine = string.Concat(fileLine, " ");

                    var isTypeOfComplicatedStructureDefined = false;

                    foreach (Match match in Regex.Matches(fileLine, Pattern))
                    {
                        AddWordWithSeparatorToSentence(sentence, match.Groups[1].ToString(),
                            match.Groups[2].ToString(), ref isTypeOfComplicatedStructureDefined);

                        if (!Separator.IsSentenceSeparator(match.Groups[2].ToString())) continue;

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
            }
            catch (IOException exception)
            {
                throw;
            }

            return text;
        }

        public ISentence Parse(string inputLine)
        {
            var sentence = new Sentence();

            var line = inputLine;

            line = string.Concat(line, " ");

            var isTypeOfComplicatedStructureDefined = false;

            foreach (Match match in Regex.Matches(line, Pattern))
            {
                AddWordWithSeparatorToSentence(sentence, match.Groups[1].ToString(), match.Groups[2].ToString(),
                    ref isTypeOfComplicatedStructureDefined);
            }

            return sentence;
        }

        private static void AddWordWithSeparatorToSentence(ISentence sentence, string word, string separator,
            ref bool isTypeOfComplicatedStructureDefined)
        {
            sentence.Add(new Word(word));

            sentence.Add(new Separator(separator));

            if (isTypeOfComplicatedStructureDefined) return;

            if (Separator.IsWordSeparator(separator))
            {
                sentence.TypeOfComplicatingStructures = TypeOfComplicatingStructures.ComplicatedSentence;
                isTypeOfComplicatedStructureDefined = true;
            }
            else
            {
                sentence.TypeOfComplicatingStructures = TypeOfComplicatingStructures.UncomplicatedSentence;
            }
        }
    }
}
