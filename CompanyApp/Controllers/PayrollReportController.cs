using CompanyApp.Data;
using CompanyApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Controllers;

public class PayrollReportController : Controller
{
    private readonly AppDbContext _context;

    public PayrollReportController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid? departmentId)
    {
        var departments = await _context.Departments
            .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
            .ToListAsync();

        var employeesQuery = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .AsQueryable();

        if (departmentId.HasValue)
        {
            employeesQuery = employeesQuery.Where(e => e.DepartmentId == departmentId);
        }

        var employees = await employeesQuery.ToListAsync();

        var report = new PayrollReportViewModel
        {
            DepartmentId = departmentId,
            Departments = departments,
            Employees = employees.Select(e => new PayrollReportEmployeeRow
            {
                FullName = $"{e.LastName} {e.FirstName} {e.MiddleName}",
                DepartmentName = e.Department?.Name,
                PositionName = e.Position?.Title,
                Salary = e.Salary
            }).ToList()
        };

        return View(report);
    }

    [HttpGet]
    public async Task<IActionResult> ExportTxt(Guid? departmentId)
    {
        var employees = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .Where(e => !departmentId.HasValue || e.DepartmentId == departmentId)
            .ToListAsync();

        var departmentName = departmentId.HasValue
            ? employees.FirstOrDefault()?.Department?.Name ?? "UnknownDepartment"
            : "AllDepartments";

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
        var safeDeptName = string.Concat(departmentName.Split(Path.GetInvalidFileNameChars()));
        var filename = $"payroll_{safeDeptName}_{timestamp}.txt";

        var reportHeader = $"Payroll Report for: {departmentName}\nGenerated at: {DateTime.Now:G}\n";

        var lines = employees.Select(e =>
            $"{e.LastName} {e.FirstName} {e.MiddleName} | {e.Department?.Name} | {e.Position?.Title} | Salary: {e.Salary:C}");

        var total = employees.Sum(e => e.Salary);
        var reportContent = reportHeader + "\n" + string.Join("\n", lines) + $"\n\nTotal Salary: {total:C}";

        var bytes = System.Text.Encoding.UTF8.GetBytes(reportContent);
        return File(bytes, "text/plain", filename);
    }
}