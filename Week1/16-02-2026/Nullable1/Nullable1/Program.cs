using System;
class program1
{
    static void Main(string[] args)
    {
        /*
        DateTime? means a nullable DateTime (Nullable<DateTime>).
        Initially dt = null
        Then assigned current date/time using DateTime.Now
        */
        DateTime? dt = null;
        dt = DateTime.Now;
        //boxing the nullable DateTime to an object
        object o = dt;
        //as operator tries to cast o to DateTime?
        DateTime? dt2 = o as DateTime?;
        if (dt2 != null)
        {
            Console.WriteLine(dt2.Value.ToString());
        }
        Console.ReadLine();
    }
}