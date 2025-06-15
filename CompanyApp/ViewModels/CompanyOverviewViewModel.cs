namespace CompanyApp.ViewModels;

public record CompanyOverviewViewModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<DepartmentEmployeesViewModel> Departments { get; set; } = [];
}