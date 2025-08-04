using System;
using System.ComponentModel.DataAnnotations;
using PowerPlant.Api.Filters;

namespace PowerPlant.Api.Dtos.Requests;

public record CreateEnergyBlockRequest(
    [Required] string Name,
    [NotPastDate] DateOnly NextServiceDate,
    [Range(1, 10000, ErrorMessage = "Поле «{0}» должно быть от {1} до {2}.")] int SensorsCount);