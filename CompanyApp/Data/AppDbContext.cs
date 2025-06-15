using CompanyApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public required DbSet<CountryEntity> Countries { get; set; }
    public required DbSet<RegionEntity> Regions { get; set; }
    public required DbSet<CityEntity> Cities { get; set; }
    public required DbSet<AddressEntity> Addresses { get; set; }

    public required DbSet<CompanyEntity> Companies { get; set; }
    public required DbSet<DepartmentEntity> Departments { get; set; }
    public required DbSet<PositionEntity> Positions { get; set; }
    public required DbSet<EmployeeEntity> Employees { get; set; }
}