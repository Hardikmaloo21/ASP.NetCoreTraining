using System;

namespace ConsoleApp
{
    class Employee
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public string Department { get; set; }

        public double salary { get; set; }

        public string Positon { get; set; }

        public DateOnly DateofJoining { get; set; }


        public void GetEmployeeData()
        {
            Console.WriteLine("Enter Employee Name: ");
            Name = Console.ReadLine();

            Console.WriteLine("Enter Employee ID: ");
            ID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Employee Department: ");
            Department = Console.ReadLine();

            Console.WriteLine("Enter the Employee Salary");
            salary = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the Employee Position");
            Positon = Console.ReadLine();

            Console.WriteLine("Enter Employee D.O.J: ");
            DateofJoining = DateOnly.Parse(Console.ReadLine());




        }

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