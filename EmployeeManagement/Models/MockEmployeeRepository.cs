using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id=1, Name="Mary", Department="HR", Email="mary@mail.com" },
                new Employee() { Id=2, Name="Bob", Department="IT", Email="bob@mail.com" },
                new Employee() { Id=3, Name="John", Department="IT", Email="John@mail.com" },
            };
  
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);

        }
    }
}
