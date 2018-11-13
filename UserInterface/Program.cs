using System;
using System.Configuration;
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

            //Console.WriteLine(text);

            foreach (var sentence in text.GetSentences())
            {
                Console.WriteLine(sentence);
            }


            //T1
            var sortedSentences = text.SortSentencesAscending<IWord>();

            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence);
            }

            //T2
            var wordLength = 3;
            var words =
                text.GetSentences(x => x.SentenceTypes.Contains(SentenceType.InterrogativeSentence))
                    .SelectMany(y => y.GetElements<IWord>(x => x.Length == wordLength)).Distinct();

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            //T3
            var wordLength2 = 3;
            foreach (var item in text.GetSentences())
            {
                item.Remove<IWord>(x => x.Length == wordLength2 && x.StartWithConsonant());
            }

            foreach (var sentence in text.GetSentences())
            {
                Console.WriteLine(sentence);
            }

            Console.ReadKey();
        }
    }
}
