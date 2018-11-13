﻿using System;
using System.Configuration;
using System.IO;
using Interfaces.TextObjectModel.SentenceElements;
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

            var sortedSentences = text.SortSentencesDescending<IWord>();

            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence);
            }

            Console.ReadKey();
        }
    }
}