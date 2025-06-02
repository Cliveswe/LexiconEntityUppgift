using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeesApp.Terminal;
internal class Program
{

    static readonly EmployeeService employeeService = new(
     new EmployeeRepository(
         new ApplicationContext(
             new DbContextOptionsBuilder<ApplicationContext>()
                 .UseSqlServer(JsonDocument.Parse(File.ReadAllText(
                     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "appsettings.json")
                 ))
                 .RootElement.GetProperty("ConnectionStrings")
                 .GetProperty("DefaultConnection")
                 .GetString())
                 .Options
         )
     )
 );
    static void Main(string[] args) {
        ListAllEmployees(employeeService);
        ListEmployee(562, employeeService);
    }

    private static void ListAllEmployees(EmployeeService employeeService) {
        foreach(var item in employeeService.GetAll()) {
            Console.Write(item.Name);
            Console.WriteLine($" {item.Salary}");
        }

        Console.WriteLine("------------------------------");
    }

    private static void ListEmployee(int employeeID, EmployeeService employeeService) {
        Employee? employee;

        try {
            employee = employeeService.GetById(employeeID);
            Console.WriteLine($"{employee?.Name}: {employee?.Email}");
            Console.WriteLine("------------------------------");
        } catch(ArgumentException e) {
            Console.WriteLine($"EXCEPTION: {e.Message}");
        }
    }
}
