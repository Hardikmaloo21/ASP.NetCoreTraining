using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class WordFrequencyAnalyzer
{
    static void Main()
    {
        string text = "The quick brown fox jumps over the lazy dog. " +
                      "The fox is quick and the dog is lazy. " +
                      "Quick brown fox jumps over the lazy dog again.";

        int N = 3;

        // Convert to lowercase and remove punctuation
        text = text.ToLower();
        text = Regex.Replace(text, @"[^\w\s]", "");

        // Split text into words
        string[] words = text.Split(
            new char[] { ' ' },
            StringSplitOptions.RemoveEmptyEntries);

        // Dictionary to store word frequencies
        Dictionary<string, int> frequency =
            new Dictionary<string, int>();

        // Count frequencies
        foreach (string word in words)
        {
            if (frequency.ContainsKey(word))
                frequency[word]++;
            else
                frequency[word] = 1;
        }

        int totalWords = words.Length;
        int uniqueWords = frequency.Count;

        Console.WriteLine("--- Word Frequency Analysis ---\n");

        Console.WriteLine("Total words: " + totalWords);
        Console.WriteLine("Unique words: " + uniqueWords);

        // Top N frequent words
        Console.WriteLine("\nTop " + N + " Frequent Words:\n");

        var topWords = frequency
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Take(N);

        foreach (var item in topWords)
        {
            Console.WriteLine(item.Key + ": " +
                              item.Value + " times");
        }

        // Words appearing exactly once
        Console.WriteLine("\nWords appearing exactly once:\n");

        var singleWords = frequency
            .Where(x => x.Value == 1)
            .Select(x => x.Key);

        Console.WriteLine(string.Join(", ", singleWords));

        // Calculate average frequency
        double averageFrequency =
            (double)totalWords / uniqueWords;

        Console.WriteLine("\nAverage frequency: " +
                          averageFrequency.ToString("0.00") +
                          " times per unique word");
    }
}