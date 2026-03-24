using System;
class Program
{
    public static void Main()
    {
        string myInput;
        int myInt;
    begin:
        Console.Write("please enter a number between 1 and 3: ");
        myInput = Console.ReadLine();
        myInt = Int32.Parse(myInput);

        switch (myInt)
        {
            case 1:
                Console.WriteLine($"Your number is {1}.", myInt);
                break;
            case 2:
                Console.WriteLine($"Your number is {2}.", myInt);
                break;
            case 3:
                Console.WriteLine($"Your number is {3}.", myInt);
                break;
            default:
                Console.WriteLine("You have entered an invalid number");
                break;
        }
    }
}