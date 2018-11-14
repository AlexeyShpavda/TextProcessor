using System;
using System.Configuration;
using System.IO;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;
using SyntacticalAnalyzer;

namespace UserInterface
{
    internal class Program
    {
        private static void Main()
        {
            var readFilePath = ConfigurationManager.AppSettings["readFilePath"];

            var textParser = new TextParser();

            try
            {
                IText text;
                using (var streamReader = new StreamReader(readFilePath))
                {
                    text = textParser.Parse(streamReader);
                }

                Console.WriteLine("<==================== Initial Text ====================>");
                Console.WriteLine(text);

                Console.WriteLine("<======================= Task1 ========================>");

                #region Realization

                var sortedSentences = text.SortSentencesAscending<IWord>();
                foreach (var sentence in sortedSentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                Console.WriteLine("<======================= Task2 ========================>");

                #region Realization

                var wordLength = 5;
                var words = text.GetWordsFromSentences(SentenceType.InterrogativeSentence, wordLength);
                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }
            
                #endregion

                Console.WriteLine("<======================= Task3 ========================>");

                #region Realization

                var wordLength2 = 5;
                text.DeleteWordsStartingWithConsonant(wordLength2);

                foreach (var sentence in text.GetSentences())
                {
                    sentence.SentenceUpdate(textParser.Parse(sentence.ToString()).GetElements<ISentenceElement>());
                    Console.WriteLine(sentence);
                }

                #endregion

                Console.WriteLine("<======================= Task4 ========================>");

                #region Realization

                var sentenceNumber = 1;
                var wordLength3 = 4;
                text.ReplacesWordsInSentenceWithSubstring(sentenceNumber, wordLength3,
                    textParser.Parse("peck, beak        peck").GetElements<ISentenceElement>());
                text.GetSentenceByIndex(sentenceNumber - 1).SentenceUpdate(textParser
                    .Parse(text.GetSentenceByIndex(sentenceNumber - 1).ToString()).GetElements<ISentenceElement>());

                foreach (var sentence in text.GetSentences())
                {
                    Console.WriteLine(sentence);
                }

                #endregion

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
