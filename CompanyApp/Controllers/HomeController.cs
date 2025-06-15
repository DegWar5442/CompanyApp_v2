using Microsoft.AspNetCore.Mvc;
using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;
using CompanyApp.ViewModels;

namespace CompanyApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var company = await _context.Companies
            .Select(c => new CompanyOverviewViewModel
            {
                Name = c.Name,
                Description = c.Description,
                Departments = c.Departments.Select(d => new DepartmentEmployeesViewModel
                {
                    DepartmentName = d.Name,
                    EmployeeCount = d.Employees.Count
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return View(company);
    }
}