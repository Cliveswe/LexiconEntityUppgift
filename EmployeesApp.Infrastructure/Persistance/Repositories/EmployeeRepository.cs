using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;

namespace EmployeesApp.Infrastructure.Persistance.Repositories
{
    public class EmployeeRepository(ApplicationContext applicationContext) : IEmployeeRepository
    {

        public void Add(Employee employee)
        {
            applicationContext.Employees.Add(employee);
            applicationContext.SaveChanges();
        }

        //Classic C# syntax for GetAll()
        public Employee[] GetAll()
        {
            return [.. applicationContext.Employees];
        }

        public Employee? GetById(int id) => applicationContext.Employees
            .Find(id);
    }
}