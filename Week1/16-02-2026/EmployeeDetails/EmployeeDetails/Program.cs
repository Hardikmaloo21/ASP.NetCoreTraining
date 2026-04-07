using System;

namespace ConsoleApp
{
    class Employee
    {

        /*

        this are data members of the class Employee
        These are auto-implemented properties:

        Store employee details
        get → read value
        set → assign value

        */
        public string Name { get; set; }

        public int ID { get; set; }

        public string Department { get; set; }

        public double Salary { get; set; }

        public string Position { get; set; }

        public DateOnly DateofJoining { get; set; }

        //this is method to get employee data from user input
        public void GetEmployeeData()
        {
            Console.WriteLine("Enter Employee Name: ");
            Name = Console.ReadLine();

            Console.WriteLine("Enter Employee ID: ");
            ID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Employee Department: ");
            Department = Console.ReadLine();

            Console.WriteLine("Enter the Employee Salary");
            Salary = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the Employee Position");
            Position = Console.ReadLine();

            Console.WriteLine("Enter Employee D.O.J: ");
            DateofJoining = DateOnly.Parse(Console.ReadLine());




        }
        
        //this is method to display employee data
        public void DisplayEmployeeData()
        {
            Console.WriteLine($"Employee Name: {Name}");
            Console.WriteLine($"Employee ID: {ID}");
            Console.WriteLine($"Employee Department: {Department}");
            Console.WriteLine($"Employee Salary: {salary}");
            Console.WriteLine($"Employee Position: {Positon}");
            Console.WriteLine($"Employee Date of Joining: {DateofJoining}");

        }

        class Program
        {
            static void Main(string[] args)
            {
                Employee emp = new Employee();
                emp.GetEmployeeData();
                emp.DisplayEmployeeData();
            }
        }
    }
}