using System;
using System.Collections.Generic;
using System.Linq;

class StudentGradeProcessor
{
    static void Main()
    {
        // Student names and grades
        Dictionary<string, int[]> students = new Dictionary<string, int[]>
        {
            { "John",  new int[] { 85, 90, 78, 92 } },
            { "Sarah", new int[] { 95, 88, 91, 89 } },
            { "Mike",  new int[] { 70, 65, 80, 75 } },
            { "Emma",  new int[] { 88, 92, 94, 96 } }
        };

        Console.WriteLine("--- Student Grade Report ---\n");

        string topPerformer = "";
        double highestAverage = 0;

        // HashSet for unique grades
        HashSet<int> uniqueGrades = new HashSet<int>();

        // Process each student
        foreach (var student in students)
        {
            string name = student.Key;
            int[] grades = student.Value;

            double average = grades.Average();
            int highest = grades.Max();
            int lowest = grades.Min();

            // Add grades to HashSet
            foreach (int grade in grades)
            {
                uniqueGrades.Add(grade);
            }

            // Find top performer
            if (average > highestAverage)
            {
                highestAverage = average;
                topPerformer = name;
            }

            Console.WriteLine(
                name +
                ": Average = " + average.ToString("0.00") +
                ", Highest = " + highest +
                ", Lowest = " + lowest
            );
        }

        // Top performer
        Console.WriteLine("\nTop Performer: " +
            topPerformer +
            " (Average: " +
            highestAverage.ToString("0.00") + ")");

        // Students with all grades >= 80
        Console.WriteLine("\nStudents with all grades >= 80:\n");

        foreach (var student in students)
        {
            string name = student.Key;
            int[] grades = student.Value;

            bool allAbove80 = grades.All(g => g >= 80);

            if (allAbove80)
            {
                Console.WriteLine(
                    name + " (" +
                    string.Join(",", grades) + ")"
                );
            }
        }

        // Unique grade values
        Console.WriteLine("\nUnique Grade Values Across All Students:\n");

        var sortedGrades = uniqueGrades.OrderBy(g => g);

        Console.WriteLine(string.Join(",", sortedGrades));

        Console.WriteLine(
            "\nTotal unique grades: " +
            uniqueGrades.Count
        );
    }
}