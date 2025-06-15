namespace CompanyApp.Data.Entities;

public record AddressEntity
{
    public Guid Id { get; set; }
    public required string Street { get; set; }
    public required string BuildingNumber { get; set; }
    public string? ApartmentNumber { get; set; }

    public Guid CityId { get; set; }
    public CityEntity? City { get; set; }

    public List<EmployeeEntity> Residents { get; set; } = [];
}