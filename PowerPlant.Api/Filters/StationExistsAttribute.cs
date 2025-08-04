using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PowerPlant.Api.Data;

namespace PowerPlant.Api.Filters;

public class StationExistsAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (!context.RouteData.Values.TryGetValue("stationId", out var stationIdValue))
        {
            context.Result = new BadRequestObjectResult("Id станции не указан!");
            return;
        }
        
        if (!int.TryParse(stationIdValue?.ToString(), out var stationId))
        {
            context.Result = new BadRequestObjectResult("Id станции имеет нечисловой формат!");
            return;
        }

        var db = context.HttpContext.RequestServices.GetRequiredService<PowerPlantContext>();

        var exists = await db.Stations
            .AsNoTracking()
            .AnyAsync(s => s.Id == stationId, context.HttpContext.RequestAborted);

        if (!exists)
        {
            context.Result = new NotFoundObjectResult("Станция не найдена!");
            return;
        }

        await next();
    }
}