using EMS.InMemoryAPI.Models;
namespace EMS.InMemoryAPI.Repositories
{
    public class EmployeeRepository
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, Name = "John Doe", Department = "HR", Email = "john.doe@example.com", Salary = 50000 },
            new Employee { Id = 2, Name = "Jane Smith", Department = "IT", Email = "jane.smith@example.com", Salary = 60000 },
        };
        public static List<Employee> GetAll() => _employees;
        public static Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public static void Add(Employee employee)
        {
            employee.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
        }

        public static bool Update(int id, Employee updatedEmployee)
        {
            var employee = GetById(id);
            if (employee == null) return false;

            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;
            employee.Email = updatedEmployee.Email;
            employee.Salary = updatedEmployee.Salary;
            return true;

        }
        public static bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee == null) return false;
            _employees.Remove(employee);
            return true;
        }
    }
}