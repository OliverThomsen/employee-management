using System;
using System.Collections;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable GetAllEmployees();
        Employee addEmployee(Employee employee);
    }
}
