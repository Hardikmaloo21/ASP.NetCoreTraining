using System;
class ODLExercise
{
    private int number;
    public int Number { get { return number; } }

    public ODLExercise()
    {
        Random r = new Random();
        number = r.Next();

    }

    public ODLExercise(int seed)
    {
        Random r = new Random(seed);
        number = r.Next();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ODLExercise num = new ODLExercise();
        Console.WriteLine(" Static number =  " + num.Number);
        ODLExercise num1 = new ODLExercise(500);
        Console.WriteLine(" Static Speed =  " + num1.Number);
    }
}