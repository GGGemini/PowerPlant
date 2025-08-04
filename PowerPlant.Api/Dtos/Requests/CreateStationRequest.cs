using System.ComponentModel.DataAnnotations;

namespace PowerPlant.Api.Dtos.Requests;

public record CreateStationRequest(
    [Required] string Name);