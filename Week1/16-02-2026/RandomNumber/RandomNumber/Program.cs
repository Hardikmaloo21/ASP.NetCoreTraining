using System;
class ODLExercise
{
    private int number;
    public int Number { get { return number; } }

    public ODLExercise()
    {
        Random r = new Random();
        // r.Next() → returns a random integer
        number = r.Next();

    }

    public ODLExercise(int seed)
    {
        Random r = new Random(seed);
        // Same seed → same sequence
        number = r.Next();
    }
}

class Program
{
    static void Main(string[] args)
    {
        //First value changes every run
        // Second value stays consistent (because of seed)
        ODLExercise num = new ODLExercise();
        Console.WriteLine(" Static number =  " + num.Number);
        ODLExercise num1 = new ODLExercise(500);
        Console.WriteLine(" Static Speed =  " + num1.Number);
    }
}