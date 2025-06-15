using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyApp.ViewModels;

public record PayrollReportEmployeeRow
{
    public required string FullName { get; set; }
    public required string DepartmentName { get; set; }
    public required string PositionName { get; set; }
    public decimal Salary { get; set; }
}

public record PayrollReportViewModel
{
    public Guid? DepartmentId { get; set; }
    public List<SelectListItem> Departments { get; set; } = [];

    public List<PayrollReportEmployeeRow> Employees { get; set; } = [];

    public decimal TotalSalary => Employees.Sum(e => e.Salary);
}
