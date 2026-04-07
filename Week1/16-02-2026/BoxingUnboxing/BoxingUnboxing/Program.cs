using System;
class Program
{
    static void Main(string[] args)
    {
        int marks = 85;
        Console.WriteLine("Marks: " + marks);
        //boxing means converting value type to reference type
        //the value of marks is stored in heap memory and the reference to that value is stored in objmarks
        object objmarks = marks;
        Console.WriteLine("Object Marks: " + objmarks);

        //Unboxing = extracting the value type from the object.
        // Requires explicit casting (int).
        // A new copy of the value (85) is created.
        //unboxmarks becomes 90.
        // This does not change objmarks, because unboxing creates a separate copy.
        int unboxmarks = (int)objmarks;
        Console.WriteLine("Unboxed Marks: " + unboxmarks);
        unboxmarks += 5;
        Console.WriteLine("Modified Unboxed Marks: " + unboxmarks);

    }
}
