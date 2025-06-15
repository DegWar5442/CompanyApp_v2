using Microsoft.EntityFrameworkCore;
using CompanyApp.Data;
using CompanyApp.Data.Entities;

namespace CompanyApp.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();
        //await db.Database.EnsureDeletedAsync(); 
        await db.Database.MigrateAsync();
    }

    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await SeedCountriesAsync(context);
        await SeedRegionsAsync(context);
        await SeedCitiesAsync(context);
        await SeedAddressesAsync(context);

        await SeedPositionsAsync(context);
        await SeedCompaniesAsync(context);
        await SeedDepartmentsAsync(context);
        await SeedEmployeesAsync(context);

    }

    private static async Task SeedCountriesAsync(AppDbContext context)
    {
        if (!await context.Countries.AnyAsync())
        {
            await context.Countries.AddAsync(new CountryEntity
            {
                Id = AppConstants.Country.Ukraine,
                Name = "Ukraine",
                Code = "UA"
            });
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedRegionsAsync(AppDbContext context)
    {
        if (!await context.Regions.AnyAsync())
        {
            await context.Regions.AddRangeAsync(
            [
                new RegionEntity { Id = AppConstants.Region.Kyivska, Name = "Kyivska", CountryId = AppConstants.Country.Ukraine },
                new RegionEntity { Id = AppConstants.Region.Lvivska, Name = "Lvivska", CountryId = AppConstants.Country.Ukraine },
                new RegionEntity { Id = AppConstants.Region.Odeska, Name = "Odeska", CountryId = AppConstants.Country.Ukraine }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCitiesAsync(AppDbContext context)
    {
        if (!await context.Cities.AnyAsync())
        {
            await context.Cities.AddRangeAsync(
            [
                new CityEntity { Id = AppConstants.City.Kyiv, Name = "Kyiv", RegionId = AppConstants.Region.Kyivska },
                new CityEntity { Id = AppConstants.City.Lviv, Name = "Lviv", RegionId = AppConstants.Region.Lvivska },
                new CityEntity { Id = AppConstants.City.Odesa, Name = "Odesa", RegionId = AppConstants.Region.Odeska },
                new CityEntity { Id = AppConstants.City.Rivne, Name = "Rivne", RegionId = AppConstants.Region.Kyivska }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAddressesAsync(AppDbContext context)
    {
        if (!await context.Addresses.AnyAsync())
        {
            await context.Addresses.AddRangeAsync(
            [
                new AddressEntity { Id = AppConstants.Address.Main1, Street = "Main", BuildingNumber = "1", CityId = AppConstants.City.Kyiv },
                new AddressEntity { Id = AppConstants.Address.Main2, Street = "Central", BuildingNumber = "2", CityId = AppConstants.City.Lviv },
                new AddressEntity { Id = AppConstants.Address.Main3, Street = "Liberty", BuildingNumber = "3", CityId = AppConstants.City.Odesa },
                new AddressEntity { Id = AppConstants.Address.Main4, Street = "Green", BuildingNumber = "4", CityId = AppConstants.City.Rivne }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedPositionsAsync(AppDbContext context)
    {
        if (!await context.Positions.AnyAsync())
        {
            await context.Positions.AddRangeAsync(
            [
                new PositionEntity { Id = AppConstants.Position.Engineer, Title = "Software Engineer" },
                new PositionEntity { Id = AppConstants.Position.HrManager, Title = "HR Manager" },
                new PositionEntity { Id = AppConstants.Position.Accountant, Title = "Accountant" },
                new PositionEntity { Id = AppConstants.Position.Designer, Title = "Graphic Designer" }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedDepartmentsAsync(AppDbContext context)
    {
        if (!await context.Departments.AnyAsync())
        {
            await context.Departments.AddRangeAsync(
            [
                new DepartmentEntity { Id = AppConstants.Department.IT, Name = "IT Department", CompanyId = AppConstants.Company.TestCompany },
                new DepartmentEntity { Id = AppConstants.Department.HR, Name = "HR Department", CompanyId = AppConstants.Company.TestCompany },
                new DepartmentEntity { Id = AppConstants.Department.Accounting, Name = "Accounting Department", CompanyId = AppConstants.Company.TestCompany }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedEmployeesAsync(AppDbContext context)
    {
        if (!await context.Employees.AnyAsync())
        {
            await context.Employees.AddRangeAsync(
            [
                new EmployeeEntity { Id = AppConstants.Employee.Alice, FirstName = "Alice", LastName = "Smith", PhoneNumber = "+380931112233", DateOfBirth = new DateTime(1990,1,1), HireDate = DateTime.UtcNow, Salary = 5000, AddressId = AppConstants.Address.Main1, DepartmentId = AppConstants.Department.IT, PositionId = AppConstants.Position.Engineer },
                new EmployeeEntity { Id = AppConstants.Employee.Bob, FirstName = "Bob", LastName = "Jones", PhoneNumber = "+380931112234", DateOfBirth = new DateTime(1988,5,15), HireDate = DateTime.UtcNow, Salary = 4800, AddressId = AppConstants.Address.Main2, DepartmentId = AppConstants.Department.HR, PositionId = AppConstants.Position.HrManager },
                new EmployeeEntity { Id = AppConstants.Employee.Carol, FirstName = "Carol", LastName = "Miller", PhoneNumber = "+380931112235", DateOfBirth = new DateTime(1992,3,22), HireDate = DateTime.UtcNow, Salary = 4700, AddressId = AppConstants.Address.Main3, DepartmentId = AppConstants.Department.Accounting, PositionId = AppConstants.Position.Accountant },
                new EmployeeEntity { Id = AppConstants.Employee.Dave, FirstName = "Dave", LastName = "Brown", PhoneNumber = "+380931112236", DateOfBirth = new DateTime(1995,9,10), HireDate = DateTime.UtcNow, Salary = 4600, AddressId = AppConstants.Address.Main4, DepartmentId = AppConstants.Department.IT, PositionId = AppConstants.Position.Designer },
                new EmployeeEntity { Id = AppConstants.Employee.Evelyn, FirstName = "Evelyn", LastName = "White", PhoneNumber = "+380931112237", DateOfBirth = new DateTime(1991,11,30), HireDate = DateTime.UtcNow, Salary = 4900, AddressId = AppConstants.Address.Main1, DepartmentId = AppConstants.Department.HR, PositionId = AppConstants.Position.Engineer }
            ]);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCompaniesAsync(AppDbContext context)
    {
        if (!await context.Companies.AnyAsync())
        {
            await context.Companies.AddAsync(new CompanyEntity
            {
                Id = AppConstants.Company.TestCompany,
                Name = "Test Company",
                Description = "For demonstration purposes. It took too long"
            });
            await context.SaveChangesAsync();
        }
    }
}
