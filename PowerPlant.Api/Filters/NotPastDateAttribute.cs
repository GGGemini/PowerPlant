using System;
using System.ComponentModel.DataAnnotations;

namespace PowerPlant.Api.Filters;

public class NotPastDateAttribute : ValidationAttribute
{
    public NotPastDateAttribute()
        : base("Дата следующего обслуживания не может быть в прошлом.") { }

    protected override ValidationResult? IsValid(object? value,
        ValidationContext context)
    {
        if (value is null) // если поле необязательно — пропускаем
            return ValidationResult.Success;

        if (value is not DateOnly date)
            return new ValidationResult("Неверный формат даты.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        return date < today
            ? new ValidationResult(ErrorMessage)
            : ValidationResult.Success;
    }
}