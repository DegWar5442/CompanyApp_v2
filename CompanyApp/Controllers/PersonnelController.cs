using CompanyApp.Data;
using CompanyApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApp.Data.Entities;

namespace CompanyApp.Controllers;

public class PersonnelController : Controller
{
    private readonly AppDbContext _context;

    public PersonnelController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? fullName, Guid? positionId, Guid? departmentId)
    {
        var employeesQuery = _context.Employees
            .Include(e => e.Position)
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(fullName))
            employeesQuery = employeesQuery.Where(e => e.FirstName.Contains(fullName) || e.LastName.Contains(fullName));

        if (positionId.HasValue)
            employeesQuery = employeesQuery.Where(e => e.PositionId == positionId);

        if (departmentId.HasValue)
            employeesQuery = employeesQuery.Where(e => e.DepartmentId == departmentId);

        var viewModel = new EmployeeFilterViewModel
        {
            FullName = fullName,
            PositionId = positionId,
            DepartmentId = departmentId,
            Employees = await employeesQuery.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}{(string.IsNullOrWhiteSpace(e.MiddleName) ? "" : $" {e.MiddleName}")}",
                PositionName = e.Position != null ? e.Position.Title : "Could not load position",
                DepartmentName = e.Department != null ? e.Department.Name : "Could not load department",
                Salary = e.Salary
            }).ToListAsync(),
            Positions = await _context.Positions.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Title
            }).ToListAsync(),
            Departments = await _context.Departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToListAsync()
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var employee = await _context.Employees
         .Include(e => e.Address)
             .ThenInclude(a => a!.City)
                 .ThenInclude(c => c!.Region)
                     .ThenInclude(r => r!.Country)
         .Include(e => e.Department)
         .Include(e => e.Position)
         .FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null)
        {
            return NotFound();
        }

        var model = new EmployeeEditViewModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleName = employee.MiddleName,
            DateOfBirth = employee.DateOfBirth,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            PhoneNumber = employee.PhoneNumber,
            DepartmentId = employee.DepartmentId,
            PositionId = employee.PositionId,
            AddressDetails = new AddressViewModel
            {
                Id = employee.Address?.Id ?? Guid.Empty, 
                Street = employee.Address?.Street ?? string.Empty,
                BuildingNumber = employee.Address?.BuildingNumber ?? string.Empty,
                ApartmentNumber = employee.Address?.ApartmentNumber,
                CityId = employee.Address?.CityId ?? Guid.Empty, 
            },
            Departments = await _context.Departments
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToListAsync(),
            Positions = await _context.Positions
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Title })
                .ToListAsync(),
            Cities = await _context.Cities
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToListAsync(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Departments = await _context.Departments
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToListAsync();
            model.Positions = await _context.Positions
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Title })
                .ToListAsync();
            model.Cities = await _context.Cities
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToListAsync();

            return View(model);
        }

        var employee = await _context.Employees
            .Include(e => e.Address) 
            .FirstOrDefaultAsync(e => e.Id == model.Id);

        if (employee is null)
        {
            return NotFound();
        }
        if (model.AddressDetails == null)
        {
            model.AddressDetails = new AddressViewModel
            {
                Id = Guid.Empty, 
                Street = string.Empty,
                BuildingNumber = string.Empty,
                CityId = Guid.Empty
            };
        }
        employee.FirstName = model.FirstName;
        employee.LastName = model.LastName;
        employee.MiddleName = model.MiddleName;
        employee.DateOfBirth = model.DateOfBirth;
        employee.HireDate = model.HireDate;
        employee.Salary = model.Salary;
        employee.PhoneNumber = model.PhoneNumber;
        employee.Address.Street = model.AddressDetails.Street;
        employee.Address.BuildingNumber = model.AddressDetails.BuildingNumber;
        employee.Address.ApartmentNumber = model.AddressDetails.ApartmentNumber;
        employee.Address.CityId = model.AddressDetails.CityId;
        employee.DepartmentId = model.DepartmentId;
        employee.PositionId = model.PositionId;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new EmployeeCreateViewModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            PhoneNumber = string.Empty,
            Cities = await _context.Cities 
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToListAsync(),
            NewAddress = new AddressViewModel { 
                Street = string.Empty,         
                BuildingNumber = string.Empty,  
                CityId = Guid.Empty             
            },
            Departments = await _context.Departments
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToListAsync(),
            Positions = await _context.Positions
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Title })
                .ToListAsync(),
            HireDate = DateTime.Today,
            DateOfBirth = DateTime.Today.AddYears(-25),
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
           
            model.Departments = await _context.Departments.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToListAsync();
            model.Positions = await _context.Positions.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Title }).ToListAsync();
            model.Cities = await _context.Cities.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToListAsync();
            return View(model);
        }
        var newAddress = new AddressEntity
        {
            Id = Guid.NewGuid(),
            Street = model.NewAddress!.Street, 
            BuildingNumber = model.NewAddress.BuildingNumber,
            ApartmentNumber = model.NewAddress.ApartmentNumber,
            CityId = model.NewAddress.CityId
        };
        _context.Addresses.Add(newAddress);
        await _context.SaveChangesAsync();
        var employee = new EmployeeEntity
        {
            Id = Guid.NewGuid(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            DateOfBirth = model.DateOfBirth,
            HireDate = model.HireDate,
            Salary = model.Salary,
            PhoneNumber = model.PhoneNumber,
            AddressId = newAddress.Id,
            DepartmentId = model.DepartmentId,
            PositionId = model.PositionId
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}