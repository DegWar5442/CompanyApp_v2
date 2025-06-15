using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyApp.ViewModels;

public record EmployeeFilterViewModel
{
    public string? FullName { get; set; }
    public Guid? PositionId { get; set; }
    public Guid? DepartmentId { get; set; }

    public List<SelectListItem> Positions { get; set; } = [];
    public List<SelectListItem> Departments { get; set; } = [];
    public List<EmployeeViewModel> Employees { get; set; } = [];
}