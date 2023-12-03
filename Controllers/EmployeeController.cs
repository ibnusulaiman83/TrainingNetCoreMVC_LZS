using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTraining2.Data;
using MyTraining2.Models;

namespace MyTraining2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyAppDbContext _context;

        public EmployeeController(MyAppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.Include("Departments");
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
           LoadDepartments();
            return View();
        }

        [NonAction]
        private void LoadDepartments()
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name" );
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid) 
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Detail(int? id) 
        {
            if (id != null) {
                NotFound();
            }
            var employee = _context.Employees.Find(id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            if (id !=null)
            {
                NotFound();
            }

            LoadDepartments();
            var employee = _context.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee model) 
        {
            ModelState.Remove("Departments");

            if (ModelState.IsValid)
            { 
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id !=null)
            {
                NotFound();
            }
            LoadDepartments();
            var employee = _context.Employees.Find(id);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee model)
        {
            _context.Employees.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
