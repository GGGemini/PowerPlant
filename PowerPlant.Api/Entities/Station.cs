using System;
using System.Collections.Generic;

namespace PowerPlant.Api.Entities;

public class Station
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public ICollection<EnergyBlock> EnergyBlocks { get; set; } = new List<EnergyBlock>();
}