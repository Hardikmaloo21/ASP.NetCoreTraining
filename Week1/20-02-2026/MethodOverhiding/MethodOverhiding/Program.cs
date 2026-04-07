using System;
class GroupAgent
{
    public void Show()
    {
        Console.WriteLine("GroupAgent Created !!");
        Console.ReadLine();
    }
}

class Agent : GroupAgent
{
    public new void Show()
    {
        Console.WriteLine("Agent Created !!");
        Console.ReadLine();
    }
}

class Exercise
{
    public static void Main(string[] args)
    {
        GroupAgent g1 = new GroupAgent();
        g1.Show();
        Agent a1 = new Agent();
        a1.Show();
        GroupAgent g2 = new Agent();
        g2.Show();
    }
}