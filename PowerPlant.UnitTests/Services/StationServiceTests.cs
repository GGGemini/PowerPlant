using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Services;

namespace PowerPlant.UnitTests.Services;

public class StationServiceTests
{
    private readonly IMapper _mapper;

    public StationServiceTests()
    {
        var cfg = new MapperConfiguration(c =>
            c.AddMaps(typeof(Program).Assembly),
            NullLoggerFactory.Instance);
        _mapper = cfg.CreateMapper();
    }
    
    [Fact]
    public async Task CreateAsync_AddsStation_AndReturnsDto()
    {
        // arrange
        await using var ctx = TestDbContextFactory.Create();
        var service = new StationService(ctx, _mapper);

        const string stationName = "New Station";
        var req = new CreateStationRequest(stationName);

        var beforeCount = await ctx.Stations.CountAsync();

        // act
        var dto = await service.CreateAsync(req, CancellationToken.None);

        // assert
        dto.Id.Should().BeGreaterThan(0);
        dto.Name.Should().Be(stationName);

        (await ctx.Stations.CountAsync())
            .Should().Be(beforeCount + 1);
    }
}