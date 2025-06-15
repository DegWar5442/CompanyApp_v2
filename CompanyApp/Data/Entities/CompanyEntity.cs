﻿namespace CompanyApp.Data.Entities;

public record CompanyEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public List<DepartmentEntity> Departments { get; set; } = [];
}