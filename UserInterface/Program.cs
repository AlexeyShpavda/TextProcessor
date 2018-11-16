using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
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
                var textParser = new TextParser();

                var txtInputFilePath = ConfigurationManager.AppSettings["txtInputFilePath"];
                var xmlOutputFilePath = ConfigurationManager.AppSettings["xmlOutputFilePath"];

                IText text;

                var textFormatting = new TextFormatting();

                using (var streamReader = new StreamReader($"{txtInputFilePath}1.txt"))
                {
                    text = textParser.Parse(streamReader);
                }

                Console.WriteLine("<==================== Initial Text ====================>");
                Console.WriteLine(text);

                Console.WriteLine("<======================= Task1 ========================>");

                #region Realization

                var sortedSentences = textFormatting.SortSentencesAscending<IWord>(text);

                foreach (var sentence in sortedSentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                Console.WriteLine("<======================= Task2 ========================>");

                #region Realization

                var wordLengthForSecondTask = 5;
                var wordsForSecondTask = textFormatting.GetWordsFromSentences(text, SentenceType.InterrogativeSentence, wordLengthForSecondTask);

                foreach (var word in wordsForSecondTask)
                {
                    Console.WriteLine(word);
                }
            
                #endregion

                Console.WriteLine("<======================= Task3 ========================>");

                #region Realization

                var wordLengthForThirdTask = 5;
                textFormatting.DeleteWordsStartingWithConsonant(text, wordLengthForThirdTask);

                foreach (var sentence in text.Sentences)
                {
                    sentence.SentenceElements = textParser.Parse(sentence.ToString());
                    Console.WriteLine(sentence);
                }

                #endregion

                Console.WriteLine("<======================= Task4 ========================>");

                #region Realization

                var sentenceNumberForFourthTask = 1;
                var wordLengthForFourthTask = 4;
                var substringForFourthTask = "peck, beak        peck";

                textFormatting.ReplacesWordsInSentenceWithSubstring(text, sentenceNumberForFourthTask,
                    wordLengthForFourthTask, textParser.Parse(substringForFourthTask));
                text.Sentences[sentenceNumberForFourthTask - 1].SentenceElements =
                    textParser.Parse(text.Sentences[sentenceNumberForFourthTask - 1].ToString());

                foreach (var sentence in text.Sentences)
                {
                    Console.WriteLine(sentence);
                }

                #endregion

                var xmlSerializer = new XmlSerializer(typeof(Text));

                using (var fileStream = new FileStream($"{xmlOutputFilePath}1.xml", FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fileStream, text);
                }
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadKey();
        }
    }
}
