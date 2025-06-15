namespace CompanyApp.Data.Entities;

public record DepartmentEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public Guid CompanyId { get; set; }
    public CompanyEntity? Company { get; set; }

    public List<EmployeeEntity> Employees { get; set; } = [];
}