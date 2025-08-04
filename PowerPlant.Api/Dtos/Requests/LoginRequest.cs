using System.ComponentModel.DataAnnotations;

namespace PowerPlant.Api.Dtos.Requests;

public record LoginRequest(
    [Required] string Email,
    [Required] string Password);