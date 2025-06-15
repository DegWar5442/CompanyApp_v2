using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.ViewModels;

public record EmployeeEditViewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public string? MiddleName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public decimal Salary { get; set; }

    [Required]
    public required string PhoneNumber { get; set; }

    [Required]
    public Guid AddressId { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }

    [Required]
    public Guid PositionId { get; set; }

    [Required]
    public AddressViewModel? AddressDetails { get; set; }

    public List<SelectListItem> Cities { get; set; } = [];
    public List<SelectListItem> Departments { get; set; } = [];
    public List<SelectListItem> Positions { get; set; } = [];
}