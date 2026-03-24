using System;
class Check_even_odd

{
    public static void Main()
    {
        int number1;
        int number2;
        Console.WriteLine("Enter the first number: ");
        number1 = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the second number: ");
        number2 = Convert.ToInt32(Console.ReadLine());

        if (number1 % 2 == 0 && number2 % 2 == 0 || number1 % 2 != 0 && number2 % 2 != 0)
        {
            Console.WriteLine("True");
        }
        else
        {
            Console.WriteLine("False");
        }


    }

}