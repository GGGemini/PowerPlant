using System;
using Microsoft.EntityFrameworkCore;
using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Data;

public class PowerPlantContext : DbContext
{
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<EnergyBlock> EnergyBlocks => Set<EnergyBlock>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    public PowerPlantContext(DbContextOptions<PowerPlantContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
        
        // инициализируем начальные данные
        
        // роли
        b.Entity<Role>().HasData(SeedData.Roles);
        
        // пользователи
        b.Entity<User>().HasData(SeedData.AdminUsers);
        b.Entity<User>().HasData(SeedData.SimpleUsers);
        
        // станции
        var stationOneId = 1;
        var stationTwoId = 2;
        
        b.Entity<Station>().HasData(
            new Station { Id = stationOneId, Name = "Станция 1", CreatedAt = SeedData.SeedNow },
            new Station { Id = stationTwoId, Name = "Станция 2", CreatedAt = SeedData.SeedNow });
        
        // энергоблоки
        b.Entity<EnergyBlock>().HasData(
            // 1
            new EnergyBlock {
                Id = 1,
                StationId       = stationOneId,
                Name            = "Энергоблок 1.1",
                NextServiceDate = new DateOnly(2025, 12, 1),
                SensorsCount    = 42,
                CreatedAt       = SeedData.SeedNow,
            },
            new EnergyBlock {
                Id = 2,
                StationId       = stationOneId,
                Name            = "Энергоблок 1.2",
                NextServiceDate = new DateOnly(2026, 4, 3),
                SensorsCount    = 35,
                CreatedAt       = SeedData.SeedNow,
            },
            
            // 2
            new EnergyBlock {
                Id = 3,
                StationId       = stationTwoId,
                Name            = "Энергоблок 2.1",
                NextServiceDate = new DateOnly(2026, 1, 5),
                SensorsCount    = 50,
                CreatedAt       = SeedData.SeedNow,
            });
    }
}