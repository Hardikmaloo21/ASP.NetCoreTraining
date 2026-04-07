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

        for (int i = 0; i < width; i++)
        {
            for (int j = 1; j <= i + 1; j++)
            {
                Console.Write(number + " ");

            }

            Console.WriteLine();
        }

    }
}