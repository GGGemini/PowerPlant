using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PowerPlant.Api;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Services;
using Xunit;

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
    
    [Fact]
    public async Task UpdateAsync_UpdatesExistingStation()
    {
        // Arrange
        await using var ctx = TestDbContextFactory.Create();
        var service = new StationService(ctx, _mapper);
        var req = new CreateStationRequest("OldName");
        var dto = await service.CreateAsync(req, CancellationToken.None);
        const string newName = "NewName";

        // Act
        var updated = await service.UpdateAsyncClassicApproach(dto.Id, new UpdateStationRequest(newName), CancellationToken.None);

        // Assert
        updated.Should().BeTrue();
        var station = await ctx.Stations.FindAsync(dto.Id);
        station!.Name.Should().Be(newName);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenStationDoesNotExist()
    {
        // Arrange
        await using var ctx = TestDbContextFactory.Create();
        var service = new StationService(ctx, _mapper);

        const int nonExistentId = int.MaxValue; // такого Id, скорее всего, не будет в БД
        var request = new UpdateStationRequest("NewName");
        
        // Act
        var result = await service.UpdateAsyncClassicApproach(nonExistentId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task DeleteAsync_RemovesExistingStation()
    {
        // Arrange
        await using var ctx = TestDbContextFactory.Create();
        var service = new StationService(ctx, _mapper);
        var req = new CreateStationRequest("ToDelete");
        var dto = await service.CreateAsync(req, CancellationToken.None);

        // Act
        var deleted = await service.DeleteAsyncClassicApproach(dto.Id, CancellationToken.None);

        // Assert
        deleted.Should().BeTrue();
        (await ctx.Stations.FindAsync(dto.Id)).Should().BeNull();
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenStationDoesNotExist()
    {
        // Arrange
        await using var ctx = TestDbContextFactory.Create();
        var service = new StationService(ctx, _mapper);

        // Act
        var deleted = await service.DeleteAsyncClassicApproach(int.MaxValue, CancellationToken.None);

        // Assert
        deleted.Should().BeFalse();
    }
}