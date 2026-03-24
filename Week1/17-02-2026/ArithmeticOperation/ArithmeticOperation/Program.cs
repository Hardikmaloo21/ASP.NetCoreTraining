using System;
namespace ArithmeticOperatrions
{
    class Operations
    {
        int a, b;
        public void GetNumber()
        {
            Console.WriteLine("enter the first number: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the second number: ");
            b = Convert.ToInt32(Console.ReadLine());
        }
        public void Add()
        {
            Console.WriteLine($"Addition: {a + b}");
        }
        public void Substract()
        {
            Console.WriteLine($"Substract: {a - b}");
        }
        public void Multiply()
        {
            Console.WriteLine($"multiply: {a * b}");
        }
        public void Division()
        {
            Console.WriteLine($"Division: {a / b}");
        }



        public static void Main()
        {
            Operations operation = new Operations();
            operation.GetNumber();
            operation.Add();
            operation.Substract();
            operation.Multiply();
            operation.Division();
        }
    }
}