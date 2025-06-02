using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Infrastructure.Persistance;
using EmployeesApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeesApp.Terminal;
internal class Program
{

    static readonly EmployeeService employeeService = new(
        new EmployeeRepository(
            new ApplicationContext(
                new DbContextOptionsBuilder<ApplicationContext>()
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmployeesDb;Trusted_Connection=True;")
                    .Options
            )
        )
    );
    static void Main(string[] args)
    {
        ListAllEmployees(employeeService);
        ListEmployee(562, employeeService);
    }

    private static void ListAllEmployees(EmployeeService employeeService)
    {
        foreach (var item in employeeService.GetAll())
            Console.WriteLine(item.Name);

        Console.WriteLine("------------------------------");
    }

    private static void ListEmployee(int employeeID, EmployeeService employeeService)
    {
        Employee? employee;

        try
        {
            employee = employeeService.GetById(employeeID);
            Console.WriteLine($"{employee?.Name}: {employee?.Email}");
            Console.WriteLine("------------------------------");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"EXCEPTION: {e.Message}");
        }
    }
}
