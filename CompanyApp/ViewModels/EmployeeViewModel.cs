namespace CompanyApp.ViewModels;

public class EmployeeViewModel
{
    public required Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string PositionName { get; set; }
    public required string DepartmentName { get; set; }
    public required decimal Salary { get; set; }
}