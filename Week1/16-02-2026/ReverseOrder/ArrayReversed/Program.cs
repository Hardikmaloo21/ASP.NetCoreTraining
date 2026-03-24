using System;

class Program
{
    public static void Main()
    {

        Console.WriteLine("enter 3 letters");
        string input = Console.ReadLine();

        if (input.Length == 3)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            string reversed = new string(arr);
            Console.WriteLine("reversed letters: " + reversed);

        }
        else
        {
            Console.WriteLine("please enter exactly 3 letters");
        }
    }
}