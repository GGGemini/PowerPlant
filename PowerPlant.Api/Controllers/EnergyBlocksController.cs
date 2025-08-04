using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Filters;
using PowerPlant.Api.Services;

namespace PowerPlant.Api.Controllers;

[ApiController]
[Route("api/stations/{stationId:int}/energy-blocks")]
[Authorize]
[ServiceFilter<StationExistsAttribute>]
public class EnergyBlocksController(IEnergyBlockService service) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<EnergyBlockResponse>> GetAll(int stationId, CancellationToken ct) =>
        await service.GetAllAsync(stationId, ct);
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnergyBlockResponse>> GetById(int stationId, int id, CancellationToken ct) =>
        await service.GetByIdAsync(stationId, id, ct) is { } dto ? Ok(dto) : NotFound();
    
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<EnergyBlockResponse>> Post(
        int stationId, CreateEnergyBlockRequest req, CancellationToken ct)
    {
        var resp = await service.CreateAsync(stationId, req, ct);

        return CreatedAtAction(nameof(GetById),new { stationId, id = resp.Id }, resp); // 201
    }
    
    [HttpPut("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put(int stationId, int id, UpdateEnergyBlockRequest req, CancellationToken ct) =>
        await service.UpdateAsync(stationId, id, req, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int stationId, int id, CancellationToken ct) =>
        await service.DeleteAsync(stationId, id, ct) ? NoContent() : NotFound();
}