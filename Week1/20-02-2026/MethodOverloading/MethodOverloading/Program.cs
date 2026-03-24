
using System;

//--------------------------Method Overloading--------------------------
class Car
{
    public string Name;
    public int NumberOfDoors;
    public Car()
    {
        Name = "Maruti Suzuki";
        NumberOfDoors = 4;
    }
    public Car(string name, int numberOfDoors)
    {
        Name = name;
        NumberOfDoors = numberOfDoors;
    }
    public Car(string name)
    {
        Name = name;
        NumberOfDoors = 0;
    }
    public Car(int numberOfDoors)
    {
        Name = "";
        NumberOfDoors = numberOfDoors;
    }
}

class Exercise
{
    static void Main(string[] args)
    {

        Car c1 = new Car();
        Car c2 = new Car(2);
        Car c3 = new Car("Hyundai i20");
        Car c4 = new Car("Honda City", 4);
        Console.WriteLine(c1.NumberOfDoors);
        Console.WriteLine(c1.Name);
        Console.WriteLine(c2.NumberOfDoors);

    }
}