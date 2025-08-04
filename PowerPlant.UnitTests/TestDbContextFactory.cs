using Microsoft.EntityFrameworkCore;
using PowerPlant.Api.Data;

namespace PowerPlant.UnitTests;

public static class TestDbContextFactory
{
    public static PowerPlantContext Create()
    {
        var options = new DbContextOptionsBuilder<PowerPlantContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        var ctx = new PowerPlantContext(options);
        ctx.Database.EnsureCreated();

        return ctx;
    }
    
    
}