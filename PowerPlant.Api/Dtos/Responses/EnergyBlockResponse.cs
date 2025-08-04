namespace PowerPlant.Api.Dtos.Responses;

public record EnergyBlockResponse(int Id, string Name, DateOnly NextServiceDate, int SensorsCount, DateTime CreatedAt);