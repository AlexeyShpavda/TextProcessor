using System;
using System.Configuration;
using System.IO;
using Interfaces.TextObjectModel;
using Interfaces.TextObjectModel.SentenceElements;
using Interfaces.TextObjectModel.Sentences.Enums;
using SyntacticalAnalyzer;
using TextObjectModel;

namespace UserInterface
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var txtFilesPath = ConfigurationManager.AppSettings["txtFilesPath"];
                var xmlFilesPath = ConfigurationManager.AppSettings["xmlFilesPath"];

                var txtInputFilePath = $"{txtFilesPath}3Lines.txt";
                var xmlOutputFilePath = $"{xmlFilesPath}1.xml";

                IText text;

                var textParser = new TextParser();

                using (var streamReader = new StreamReader(txtInputFilePath))
                {
                    // Replace tabs and spaces with one space during parsing.
                    text = textParser.Parse(streamReader);
                }

                var textFormatting = new TextFormatting();

                Console.WriteLine("<==================== Initial Text ====================>");
                Console.WriteLine(text);

                // Print all sentences of text in increasing order of number of words in each of them.
                Console.WriteLine("<======================= Task1 ========================>");

                #region Realization

                var sortedSentences = textFormatting.SortSentencesAscending<IWord>(text);

                foreach (var sentence in sortedSentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                // In all interrogative sentences of text, find and print without repeating words of a given length.
                Console.WriteLine("<======================= Task2 ========================>");

                #region Realization

                var wordLengthForSecondTask = 5;
                var wordsForSecondTask = textFormatting.GetWordsFromSentences(text, SentenceType.InterrogativeSentence,
                    wordLengthForSecondTask);

                foreach (var word in wordsForSecondTask)
                {
                    Console.WriteLine(word);
                }

                #endregion

                // Remove from text all words of a given length beginning with a consonant letter.
                Console.WriteLine("<======================= Task3 ========================>");

                #region Realization

                var wordLengthForThirdTask = 5;
                text = textFormatting.DeleteWordsStartingWithConsonant(text, wordLengthForThirdTask);

                foreach (var sentence in text.Sentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                // In some sentence of text, word of a given length replace with the specified substring.
                Console.WriteLine("<======================= Task4 ========================>");

                #region Realization

                var sentenceNumberForFourthTask = 1;
                var wordLengthForFourthTask = 7;
                var substringForFourthTask = "peck. beak.        peck,";

                text = textFormatting.ReplacesWordsInSentenceWithSubstring(text, sentenceNumberForFourthTask,
                    wordLengthForFourthTask, textParser.Parse(substringForFourthTask));

                foreach (var sentence in text.Sentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                //text.SaveToXmlFile(xmlOutputFilePath);

                //text = Text.ReadFromXmlFile(xmlOutputFilePath);
            }
            //catch (ArgumentException argumentException)
            //{
            //    Console.WriteLine(argumentException.Message);
            //}
            //catch (IOException ioException)
            //{
            //    Console.WriteLine(ioException.Message);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
