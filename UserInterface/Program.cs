using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
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

            var streamReader = new StreamReader(readFilePath);
            var textParser = new TextParser();
            var text = textParser.Parse(streamReader);

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
            var wordLength2 = 3;
            text.DeleteWordsStartingWithConsonant(wordLength2);

            foreach (var sentence in text.GetSentences())
            {
                Console.WriteLine(sentence);
            }
            #endregion

            Console.WriteLine("<======================= Task4 ========================>");

            #region Realization

            #endregion

            Console.ReadKey();
        }
    }
}
