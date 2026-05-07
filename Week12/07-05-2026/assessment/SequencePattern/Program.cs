using System;
using System.Collections.Generic;
using System.Linq;

class SequencePatternDetector
{
    static void Main()
    {
        int[] accessLog = { 1, 3, 2, 3, 3, 4, 5, 3, 6, 7, 8, 9, 10, 3 };
        int K = 2;

        Console.WriteLine("--- Access Pattern Analysis ---\n");
        
        // 1. Longest Consecutive Sequence
        HashSet<int> numbers = new HashSet<int>(accessLog);
        int longestLength = 0;
        List<int> longestSequence = new List<int>();
        foreach (int num in numbers)
        {
            // Start sequence only if previous number doesn't exist
            if (!numbers.Contains(num - 1))
            {
                int current = num;
                List<int> sequence = new List<int>();

                while (numbers.Contains(current))
                {
                    sequence.Add(current);
                    current++;
                }

                if (sequence.Count > longestLength)
                {
                    longestLength = sequence.Count;
                    longestSequence = sequence;
                }
            }
        }

        Console.WriteLine("Longest Consecutive Sequence: " +
            string.Join(",", longestSequence) +
            " (Length: " + longestLength + ")\n");

        // -----------------------------
        // 2. Most Frequent Element
        // -----------------------------
        Dictionary<int, int> frequency = new Dictionary<int, int>();

        foreach (int num in accessLog)
        {
            if (frequency.ContainsKey(num))
                frequency[num]++;
            else
                frequency[num] = 1;
        }

        var mostFrequent = frequency
            .OrderByDescending(x => x.Value)
            .First();

        Console.WriteLine("Most Frequent Element: " +
            mostFrequent.Key +
            " (appears " + mostFrequent.Value + " times)\n");

        // -----------------------------
        // 3. First Non-Repeating Element
        // -----------------------------
        int firstNonRepeating = -1;

        foreach (int num in accessLog)
        {
            if (frequency[num] == 1)
            {
                firstNonRepeating = num;
                break;
            }
        }

        Console.WriteLine("First Non-Repeating Element: " +
            firstNonRepeating + "\n");

        // -----------------------------
        // 4. Pairs with Difference K
        // -----------------------------
        Console.WriteLine("Pairs with Difference " + K + ":\n");

        HashSet<string> printedPairs = new HashSet<string>();

        foreach (int num in numbers)
        {
            if (numbers.Contains(num + K))
            {
                string pair = "(" + num + ", " + (num + K) + ")";

                if (!printedPairs.Contains(pair))
                {
                    Console.Write(pair + " ");
                    printedPairs.Add(pair);
                }
            }
        }

        Console.WriteLine("\n");

        // -----------------------------
        // 5. Majority Element
        // -----------------------------
        int totalElements = accessLog.Length;

        double percentage =
            ((double)mostFrequent.Value / totalElements) * 100;

        if (mostFrequent.Value > totalElements / 2)
        {
            Console.WriteLine("Majority Element: " +
                mostFrequent.Key +
                " (appears " + mostFrequent.Value +
                " out of " + totalElements +
                " times)");
        }
        else
        {
            Console.WriteLine("Majority Element: " +
                mostFrequent.Key +
                " (appears " + mostFrequent.Value +
                " out of " + totalElements +
                " times - " +
                percentage.ToString("0.0") +
                "% - No majority)");
        }
    }
}