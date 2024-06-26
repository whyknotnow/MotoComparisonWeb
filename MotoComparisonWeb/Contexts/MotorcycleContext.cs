﻿using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

public class MotorcycleContext : DbContext
{
    public MotorcycleContext(DbContextOptions<MotorcycleContext> options) : base(options) { }

    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<MotoSpecification> Specifications { get; set; }
}

public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public List<Model> Models { get; set; }
}

public class Model
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
}

public class MotoSpecification
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public int ModelId { get; set; }
    public Model Model { get; set; }
}
