using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Interfaces.SyntacticalAnalyzer;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences;
using TextObjectModel;
using TextObjectModel.SentenceElements;
using TextObjectModel.Sentences;

namespace SyntacticalAnalyzer
{
    public class TextParser : ITextParser
    {
        private readonly string _wordSeparationPattern = ConfigurationManager.AppSettings["wordSeparationPattern"];
        private readonly string _sentencesSeparationPattern = ConfigurationManager.AppSettings["sentencesSeparationPattern"];

        public IText Parse(StreamReader streamReader)
        {
            IList<ISentence> sentences = new List<ISentence>();
            var strBuffer = string.Empty;

            string fileLine;
            while ((fileLine = streamReader.ReadLine()) != null)
            {
                fileLine = strBuffer + Regex.Replace(fileLine, "[\f\n\r\t\v]", " ");

                var strSentences = Regex.Split(fileLine, _sentencesSeparationPattern);

                foreach (var strSentence in strSentences)
                {
                    if (Separator.SentenceSeparators.Any(x => strSentence.EndsWith(x)))
                    {
                        sentences.Add(new Sentence(Parse(strSentence)));
                        strBuffer = string.Empty;
                    }
                    else
                    {
                        strBuffer = string.Concat(strSentence, " ");
                    }
                }
            }

            if (strBuffer != string.Empty)
            {
                sentences.Add(new Sentence(Parse(strBuffer)));
                //throw new Exception("There is no punctuation mark at the end of the text.");
            }

            return new Text(sentences);
        }

        public ICollection<ISentenceElement> Parse(string inputLine)
        {
            var line = string.Concat(inputLine, " ");

            ICollection<ISentenceElement> sentenceElements = new Collection<ISentenceElement>();

            foreach (Match match in Regex.Matches(line, _wordSeparationPattern))
            {
                sentenceElements.Add(new Word(match.Groups[1].ToString()));

                sentenceElements.Add(new Separator(match.Groups[2].ToString()));
            }

            return sentenceElements;
        }
    }
}
