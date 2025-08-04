using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Services;

namespace PowerPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StationsController(IStationService service) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<StationResponse>> Get(CancellationToken ct) =>
        await service.GetAllAsync(ct);
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StationResponse>> Get(int id, CancellationToken ct) =>
        await service.GetByIdAsync(id, ct) is { } dto ? Ok(dto) : NotFound();
    
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<StationResponse>> Post(CreateStationRequest req, CancellationToken ct)
    {
        var dto = await service.CreateAsync(req, ct);
        
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto); // 201
    }

    [HttpPut("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put(int id, UpdateStationRequest req, CancellationToken ct) =>
        await service.UpdateAsync(id, req, ct) ? NoContent() : NotFound();
    
    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) =>
        await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}