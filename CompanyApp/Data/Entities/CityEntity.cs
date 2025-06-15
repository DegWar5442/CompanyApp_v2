namespace CompanyApp.Data.Entities;

public record CityEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? PostalCode { get; set; }

    public Guid RegionId { get; set; }
    public RegionEntity? Region { get; set; }

    public List<AddressEntity> Addresses { get; set; } = [];
}