using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.ViewModels;

public record EmployeeCreateViewModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }

    [Precision(18, 2)]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public decimal Salary { get; set; }

    public required string PhoneNumber { get; set; }

    [Required]
    public AddressViewModel NewAddress { get; set; }

    public Guid DepartmentId { get; set; }
    public Guid PositionId { get; set; }
    

    public List<SelectListItem> Cities { get; set; } = [];
    public List<SelectListItem> Departments { get; set; } = [];
    public List<SelectListItem> Positions { get; set; } = [];
    
}