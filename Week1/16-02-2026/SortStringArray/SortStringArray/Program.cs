using System;
class Program
{
    public static void Main()
    {
        string[] stringArray = new string[5] { "Csharp", "ASP.net", "EntityFrameWork", "ADO.net", "WCF" };
        Array.Sort(stringArray);
        foreach (string str in stringArray)
        {
            Console.Write(str + " ");
        }
    }
}

