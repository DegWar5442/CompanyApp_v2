namespace CompanyApp.ViewModels;

public record DepartmentEmployeesViewModel
{
    public required string DepartmentName { get; set; }
    public int EmployeeCount { get; set; }
}
