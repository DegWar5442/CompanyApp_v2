using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Data.Entities;

public record EmployeeEntity
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }

    [Precision(18, 2)]
    public decimal Salary { get; set; }

    public required string PhoneNumber { get; set; }

    public Guid AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public Guid DepartmentId { get; set; }
    public DepartmentEntity? Department { get; set; }

    public Guid PositionId { get; set; }
    public PositionEntity? Position { get; set; }
}