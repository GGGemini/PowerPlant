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
        var adminRoleId = 1;
        var userRoleId = 2;
        
        b.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin" },
            new Role { Id = userRoleId, Name = "User" });
        
        // пользователи
        b.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Surname = "Главный",
                FirstName = "Админ",
                Patronymic = "",
                Email = "tanchikipro7777777@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("MainAdmin341"),
                RoleId = adminRoleId
            },
            new User
            {
                Id = 2,
                Surname = "Простой",
                FirstName = "Пользователь",
                Patronymic = "",
                Email = "cheburashka@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("SimpleSheburashka732"),
                RoleId = userRoleId
            });
        
        // станции
        var stationOneId = 1;
        var stationTwoId = 2;
        
        b.Entity<Station>().HasData(
            new Station { Id = stationOneId, Name = "Станция 1" },
            new Station { Id = stationTwoId, Name = "Станция 2" });
        
        // энергоблоки
        b.Entity<EnergyBlock>().HasData(
            // 1
            new EnergyBlock {
                Id = 1,
                StationId       = stationOneId,
                Name            = "Энергоблок 1.1",
                NextServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(6)),
                SensorsCount    = 42
            },
            new EnergyBlock {
                Id = 2,
                StationId       = stationOneId,
                Name            = "Энергоблок 1.2",
                NextServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(12)),
                SensorsCount    = 35
            },
            
            // 2
            new EnergyBlock {
                Id = 3,
                StationId       = stationTwoId,
                Name            = "Энергоблок 2.1",
                NextServiceDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(9)),
                SensorsCount    = 50
            });
    }
}