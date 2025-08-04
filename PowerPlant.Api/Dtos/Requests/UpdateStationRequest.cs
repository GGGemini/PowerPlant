using System.ComponentModel.DataAnnotations;

namespace PowerPlant.Api.Dtos.Requests;

public record UpdateStationRequest(
    [Required] string Name);