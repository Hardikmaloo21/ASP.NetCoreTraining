using System;
class program1
{
    static void Main(string[] args)
    {
        DateTime? dt = null;
        dt = DateTime.Now;
        object o = dt;

        DateTime? dt2 = o as DateTime?;
        if (dt2 != null)
        {
            Console.WriteLine(dt2.Value.ToString());
        }
        Console.ReadLine();
    }
}