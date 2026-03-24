using System;
class display_triangle
{
    public static void Main()
    {
        int number, width;

        Console.WriteLine("Enter the number : ");
        number = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the width of the triangle: ");
        width = Convert.ToInt32(Console.ReadLine());

        for (int i = width; i >= 1; i--)
        {
            for (int j = i; j >= 1; j--)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
        }

    }
}