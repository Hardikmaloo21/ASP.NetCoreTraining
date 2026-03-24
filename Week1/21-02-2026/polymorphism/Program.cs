//------------------------------------------method overloading------------------------------------------------------
using System;

using System.Globalization;
// class Car
// {
//     public string Name;
//     public int NumberOfDoors;
//     public Car()
//     {
//         Name = "No Name";
//         NumberOfDoors = 3;
//     }
//     public Car(string name, int NumberOfDoors)
//     {
//         Name = name;
//         this.NumberOfDoors = NumberOfDoors;
//     }
    
//     public Car(string name)
//     {
//         Name = name;
//         NumberOfDoors = 0;
//     }

//     public Car(int numberOfDoors)
//     {
//         Name = "";
//         NumberOfDoors = numberOfDoors;
//     }

//     class ODLexce
//     {
//         static void Main(string[] args)
//         {
//             Car c1 = new Car();
//             Car c2 = new Car(3);
//             Car c3 = new Car("MyName");
//             Car c4 = new Car("MyName", 4);
//             Console.WriteLine(c1.NumberOfDoors);
//             Console.WriteLine(c1.Name);
            
//         }
//     }
// }

//------------------------------------------method overhiding------------------------------------------------------

// class GroupAgent
// {
//     public void show()
//     {
//         Console.WriteLine("Group Agent created");
//         Console.ReadLine();
//     }
//     class Agent : GroupAgent
//     {
//         public new void show()
//         {
//             Console.WriteLine("Agent created");
//             Console.ReadLine();
//         }
//     }

//     class ODLExercise
//     {
//         public static void Main(string[] args)
//         {
//             GroupAgent a1 = new GroupAgent();
//             a1.show();

//             Agent b1 = new Agent();
//             b1.show();

//             GroupAgent a2 = new Agent();
//             a2.show();
//         }
//     }
// }

//------------------------------------------method overriding------------------------------------------------------

// class GroupAgent
// {
//     public virtual void show()
//     {
//         Console.WriteLine("Group Agent created");
//         Console.ReadLine();
//     }
//     class Agent : GroupAgent
//     {
//         public override void show()
//         {
//             Console.WriteLine("Agent created");
//             Console.ReadLine();
//         }
//     }

//     class ODLExercise
//     {
//         public static void Main(string[] args)
//         {
//             GroupAgent a1 = new GroupAgent();
//             a1.show();

//             Agent b1 = new Agent();
//             b1.show();

//             GroupAgent a2 = new Agent();
//             a2.show();
//         }
//     }
// }


