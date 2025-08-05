using FluentAssertions;
using Microsoft.Extensions.Configuration;
using PowerPlant.Api.Data;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Services;

namespace PowerPlant.UnitTests.Services;

public class AuthServiceTests(ConfigFixture fixture) : IClassFixture<ConfigFixture>
{
    private readonly IConfiguration _config = fixture.Config;
    
    [Fact]
    public async Task AuthenticateAsync_ReturnsToken_ForValidUser()
    {
        await using var ctx = TestDbContextFactory.Create();
        var service = new AuthService(ctx, _config);

        var req = new LoginRequest(SeedData.AdminEmail, SeedData.AdminPassword);

        var token = await service.AuthenticateAsync(req, CancellationToken.None);

        token.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task AuthenticateAsync_ReturnsNull_ForInvalidRole()
    {
        await using var ctx = TestDbContextFactory.Create();
        var service = new AuthService(ctx, _config);

        // Неизвестный пользователь
        var req = new LoginRequest("nonexistent@mail.com", "anypassword");

        var token = await service.AuthenticateAsync(req, CancellationToken.None);

        token.Should().BeNull();
    }
}