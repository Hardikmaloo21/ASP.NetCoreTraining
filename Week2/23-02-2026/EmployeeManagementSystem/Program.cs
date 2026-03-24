using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int EMPLOYEE_ID { get; set; }
        public string? EMPLOYEE_NAME { get; set; }
        public int EMPLOYEE_SALARY { get; set; }
        public string? EMPLOYEE_DEPARTMENT { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // IMPORTANT: Change EmployeeDB if your database name is different
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HardikDatabase;Trusted_Connection=True;";

            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT EMPLOYEE_ID, EMPLOYEE_NAME, EMPLOYEE_DEPARTMENT, EMPLOYEE_SALARY FROM table_1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EMPLOYEE_ID = reader.GetInt32(0),
                                EMPLOYEE_NAME = reader.IsDBNull(1) ? null : reader.GetString(1),
                                EMPLOYEE_DEPARTMENT = reader.IsDBNull(2) ? null : reader.GetString(2),
                                EMPLOYEE_SALARY = reader.GetInt32(3)
                            });
                        }
                    }
                }

                Console.WriteLine("\nEmployee List:\n");

                foreach (var emp in employees)
                {
                    Console.WriteLine($"Id: {emp.EMPLOYEE_ID}");
                    Console.WriteLine($"Name: {emp.EMPLOYEE_NAME}");
                    Console.WriteLine($"Department: {emp.EMPLOYEE_DEPARTMENT}");
                    Console.WriteLine($"Salary: {emp.EMPLOYEE_SALARY}");
                    Console.WriteLine("------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}