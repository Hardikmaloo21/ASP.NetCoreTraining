using System;
class GroupAgent
{
    public virtual void show()
    {
        Console.WriteLine("GroupAgent Created !!");
        Console.ReadLine();
    }
}

class agent : GroupAgent
{
    public sealed override void show()
    {
        Console.WriteLine("Agent Created !!");
        Console.ReadLine();
    }
}

class Booking : agent
{
    public new void show()
    {
        Console.WriteLine("Booking Created !!");
        Console.ReadLine();
    }
}

class Exercise
{
    public static void Main()
    {
        GroupAgent g1 = new GroupAgent();
        g1.show();
        agent a1 = new agent();
        a1.show();
        GroupAgent g2 = new agent();
        g2.show();
    }
}