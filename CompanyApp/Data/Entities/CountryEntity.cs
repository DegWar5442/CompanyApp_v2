namespace CompanyApp.Data.Entities;

public record CountryEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Code { get; set; }

    public List<RegionEntity> Regions { get; set; } = [];
}