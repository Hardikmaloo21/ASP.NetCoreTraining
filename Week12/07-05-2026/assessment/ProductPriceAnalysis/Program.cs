using System;
using System.Collections.Generic;
using System.Linq;

class ProductPriceAnalyzer
{
    static void Main()
    {
        int[] prices = { 299, 499, 199, 399, 599, 159, 699, 259 };
        int targetSum = 698;

        Console.WriteLine("--- Product Price Analysis ---\n");

        Console.WriteLine("Original Prices: " +
            string.Join(", ", prices));

        // -----------------------------
        // Bubble Sort
        // -----------------------------
        int[] sortedPrices = (int[])prices.Clone();

        BubbleSort(sortedPrices);

        Console.WriteLine("\nSorted Prices (Ascending): " +
            string.Join(", ", sortedPrices));

        // -----------------------------
        // Binary Search
        // -----------------------------
        Console.WriteLine("\nBinary Search Results:\n");

        int search1 = 399;
        int result1 = BinarySearch(sortedPrices, search1);

        if (result1 != -1)
            Console.WriteLine("Price " + search1 +
                " found at index " + result1);
        else
            Console.WriteLine("Price " + search1 +
                " not found");

        int search2 = 500;
        int result2 = BinarySearch(sortedPrices, search2);

        if (result2 != -1)
            Console.WriteLine("Price " + search2 +
                " found at index " + result2);
        else
            Console.WriteLine("Price " + search2 +
                " not found");

        // -----------------------------
        // Pairs with Target Sum
        // -----------------------------
        Console.WriteLine("\nPairs that sum to " +
            targetSum + ":\n");

        FindPairs(sortedPrices, targetSum);

        // -----------------------------
        // Longest Increasing Subsequence
        // -----------------------------
        List<int> lis = LongestIncreasingSubsequence(sortedPrices);

        Console.WriteLine("\nLongest Increasing Subsequence:\n");

        Console.WriteLine(
            string.Join(", ", lis) +
            " (Length: " + lis.Count + ")"
        );

        // -----------------------------
        // Statistics
        // -----------------------------
        Console.WriteLine("\nStatistics:\n");

        int lowest = sortedPrices.Min();
        int highest = sortedPrices.Max();
        double average = sortedPrices.Average();

        double median;
        int n = sortedPrices.Length;

        if (n % 2 == 0)
        {
            median = (sortedPrices[n / 2 - 1] +
                     sortedPrices[n / 2]) / 2.0;
        }
        else
        {
            median = sortedPrices[n / 2];
        }

        Console.WriteLine("Lowest Price: " + lowest);
        Console.WriteLine("Highest Price: " + highest);
        Console.WriteLine("Average Price: " +
            average.ToString("0.00"));
        Console.WriteLine("Median Price: " +
            median.ToString("0.00"));
    }

    // Bubble Sort
    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // Binary Search
    static int BinarySearch(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (arr[mid] == target)
                return mid;

            if (arr[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    // Find Pairs with Target Sum
    static void FindPairs(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left < right)
        {
            int sum = arr[left] + arr[right];

            if (sum == target)
            {
                Console.WriteLine(
                    "(" + arr[left] +
                    ", " + arr[right] + ")"
                );

                left++;
                right--;
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
    }

    // Longest Increasing Subsequence
    static List<int> LongestIncreasingSubsequence(int[] arr)
    {
        List<int> lis = new List<int>();

        lis.Add(arr[0]);

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > lis[lis.Count - 1])
            {
                lis.Add(arr[i]);
            }
        }

        return lis;
    }
}