namespace CompanyApp.Data.Entities;

public record PositionEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }

    public List<EmployeeEntity> Employees { get; set; } = [];
}