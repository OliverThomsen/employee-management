using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                Title = "Details View",
            };
            
            return View(viewModel);
        }
    }
}
