namespace Inheritance
{
    public class Person
    {
        private string name;
        private int age;
        public void GetInformation()
        {
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            age = int.Parse(Console.ReadLine());
        }

        public void DisplayInformation()
        {
            Console.WriteLine($"Welcome to Dotnet Training Mr/Ms {name} , and your age is {age}");
        }
    }
    public class Employee : Person
    {
        private string CompanyName;
        private int EmployeeID;
        private int EmployeeScore;
        public int GetEmployeeInformation()
        {
            Console.WriteLine("Enter your Company Name: ");
            CompanyName = Console.ReadLine();
            EmployeeID = int.Parse(Console.ReadLine());
            EmployeeScore = int.Parse(Console.ReadLine());
            return EmployeeScore;
        }
        public void DisplayEmployeeInformation()
        {
            Console.WriteLine($"Welcome to {CompanyName} , your Employee ID is {EmployeeID} , and your Employee Score is {EmployeeScore}");
        }
    }

    interface IDepartment
    {
        string DepartmentName { get; set; }
        void DisplayDepartmentDetails();
    }
    public class GradeLevel : Employee, IDepartment
    {

        public string DepartmentName { get; set; }
        public void CheckEligibility()
        {
            Console.WriteLine("Every employee should have above 150");
            if (this.GetEmployeeInformation() > 150)
            {
                Console.WriteLine("You are eligible ");
            }
            else
            {
                Console.WriteLine("you are not eligible");
            }
        }
        public void DisplayDepartmentDetails()
        {
            Console.WriteLine($"Your Department Name is {DepartmentName}");
        }
    }

    public class TestProgram
    {
        static void Main(string[] args)
        {
            GradeLevel gl = new GradeLevel();
            gl.GetInformation();
            gl.DisplayInformation();
            // gl.GetEmployeeInformation();
            // gl.DisplayEmployeeInformation();
            gl.DepartmentName = "IT";
            gl.CheckEligibility();
            gl.DisplayDepartmentDetails();
        }
    }
}
