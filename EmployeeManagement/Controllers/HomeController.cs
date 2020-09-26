using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public ViewResult Index()
        {
            var model = employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult Details(int id=1)
        {
            Employee model = employeeRepository.GetEmployee(id);
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                Title = "Details View",
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = this.employeeRepository.GetEmployee(id);
            EmployeeEditViewModel viewModel = new EmployeeEditViewModel
            {
                Id = id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath,
            };
            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    DeleteImage(model.ExistingPhotoPath); 
                    employee.PhotoPath = ProceesUploadPhoto(model.Photo);
                }
                employeeRepository.Update(employee);
                return RedirectToAction("details", new { id = employee.Id });
            }
            return View();
        }

        private void DeleteImage(string existingPhotoPath)
        {
            if (existingPhotoPath != null)
            {
                String filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", existingPhotoPath);
                System.IO.File.Delete(filePath);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProceesUploadPhoto(model.Photo);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }

        private string ProceesUploadPhoto(IFormFile photo)
        {
            String uniqueFileName = null;
            if (photo != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", uniqueFileName);
                photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }
    }
}
