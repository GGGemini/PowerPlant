using System;

namespace PowerPlant.Api.Entities;

public class EnergyBlock
{
    public int Id { get; set; }

    public int StationId { get; set; }

    public string Name { get; set; } = default!;

    public DateOnly NextServiceDate { get; set; }

    public int SensorsCount { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public Station Station { get; set; } = default!;
}