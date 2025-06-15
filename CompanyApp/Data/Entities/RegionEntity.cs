namespace CompanyApp.Data.Entities;

public record RegionEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public Guid CountryId { get; set; }
    public CountryEntity? Country { get; set; }

    public List<CityEntity> Cities { get; set; } = [];
}