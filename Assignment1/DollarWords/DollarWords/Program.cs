using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace DollarWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch progRuntime = new Stopwatch();
            progRuntime.Start();
            StringBuilder DollarWords = new StringBuilder();
            int wordVal = 0;
            int dWordCount = 0;
            int shortestLen = 0;
            int longestLen = 0;
            int expensiveWordVal = 0;
            String shortestWord = "";
            String longestWord = "";
            String expensiveWord = "";

            // Set the directory location
            var dirPrep = Environment.CurrentDirectory.Split("DollarWords");
            var dir = dirPrep[0];

            // Set the "words.txt" location and split the words based on new line characters
            var wordFile = File.ReadAllText(dir + "words.txt");
            var words = wordFile.Split("\r\n");

            // Search for words that total to 100
            for (int i = 0; i < words.Length; i++)
            {

                for (int j = 0; j < words[i].Length; j++)
                {
                    if ((int)Char.ToLower(words[i][j]) > 96 && (int)Char.ToLower(words[i][j]) < 123)
                    {
                        wordVal += (int)Char.ToLower(words[i][j]) - 96;
                    }
                }

                if (wordVal == 100)
                {
                    DollarWords.Append(words[i] + "\r\n");
                    dWordCount += 1;

                    if (words[i].Length < shortestLen || shortestLen != 0)
                    {
                        shortestLen = words[i].Length;
                        shortestWord = words[i];
                    }
                    if (words[i].Length > longestLen)
                    {
                        longestLen = words[i].Length;
                        longestWord = words[i];
                    }
                }

                if (wordVal > expensiveWordVal)
                {
                    expensiveWordVal = wordVal;
                    expensiveWord = words[i];
                }

                wordVal = 0;
            }

            // Write the dollar words to a new file
            File.WriteAllText(dir + "DollarWords.txt", DollarWords.ToString());

            progRuntime.Stop();


            // Write out statistics to console
            Console.WriteLine($"Dollar word count:    {dWordCount}");
            Console.WriteLine($"Program runtime:      {((double)(progRuntime.ElapsedMilliseconds)/1000).ToString()} seconds");
            Console.WriteLine($"Shortest dollar word: {shortestWord}");
            Console.WriteLine($"Shortest dollar word: {longestWord}");
            Console.WriteLine($"Most expensive word:  {expensiveWord}");
            Console.ReadLine();
        }
    }
}
