
using System.ComponentModel.DataAnnotations;

namespace CompanyApp.ViewModels;

public record AddressViewModel
{
    [Required]
    public Guid Id { get; set; } 

    [Required]
    public required string Street { get; set; }

    [Required]
    public required string BuildingNumber { get; set; }

    public string? ApartmentNumber { get; set; }

    [Required]
    public Guid CityId { get; set; }

}