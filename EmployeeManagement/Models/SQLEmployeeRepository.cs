using System;
using System.Collections;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository: IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employees.Find(Id);

        }

        public IEnumerable GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges(); 
            return employeeChanges;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
            }
            return employee;
        }
    }
}
