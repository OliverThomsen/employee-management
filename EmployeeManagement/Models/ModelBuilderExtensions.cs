using System;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Mary", Department = Department.HR, Email = "mary@mail.com" },
                    new Employee { Id = 2, Name = "Bob", Department = Department.IT, Email = "bob@mail.com" },
                    new Employee { Id = 3, Name = "John", Department = Department.IT, Email = "John@mail.com" }
                );
        }
    } 
}
