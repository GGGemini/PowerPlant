using System;

namespace PowerPlant.Api.Dtos.Responses;

public record StationResponse(int Id, string Name, int BlocksCount, DateTime CreatedAt);