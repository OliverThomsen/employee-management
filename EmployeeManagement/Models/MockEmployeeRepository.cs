using System;
using System.Collections;
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
                new Employee() { Id=1, Name="Mary", Department=Department.HR, Email="mary@mail.com" },
                new Employee() { Id=2, Name="Bob", Department=Department.IT, Email="bob@mail.com" },
                new Employee() { Id=3, Name="John", Department=Department.IT, Email="John@mail.com" },
            };

        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);

        }

        public IEnumerable GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee addEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }
    }
}
