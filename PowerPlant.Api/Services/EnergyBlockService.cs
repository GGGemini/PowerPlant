using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PowerPlant.Api.Data;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Services;

public class EnergyBlockService(PowerPlantContext db, IMapper mapper) : IEnergyBlockService
{
    public async Task<IEnumerable<EnergyBlockResponse>> GetAllAsync(int stationId, CancellationToken ct) =>
        await db.EnergyBlocks
            .Where(x => x.StationId == stationId)
            .ProjectTo<EnergyBlockResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

    public async Task<EnergyBlockResponse?> GetByIdAsync(int stationId, int id, CancellationToken ct) =>
        await db.EnergyBlocks
            .Where(x => x.StationId == stationId && x.Id == id)
            .ProjectTo<EnergyBlockResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);

    public async Task<EnergyBlockResponse> CreateAsync(int stationId, CreateEnergyBlockRequest req, CancellationToken ct)
    {
        var entity = mapper.Map<EnergyBlock>(req);
        entity.StationId = stationId;

        db.EnergyBlocks.Add(entity);
        await db.SaveChangesAsync(ct);
        
        return mapper.Map<EnergyBlockResponse>(entity);
    }

    public async Task<bool> UpdateAsync(int stationId, int id, UpdateEnergyBlockRequest req, CancellationToken ct)
    {
        var updated = await db.EnergyBlocks
            .Where(b => b.StationId == stationId && b.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(s => s.Name, req.Name)
                .SetProperty(s => s.NextServiceDate, req.NextServiceDate)
                .SetProperty(s => s.SensorsCount, req.SensorsCount),
                ct);
        
        return updated == 1;
    }

    public async Task<bool> DeleteAsync(int stationId, int id, CancellationToken ct)
    {
        var deleted = await db.EnergyBlocks
            .Where(b => b.Id == id && b.StationId == stationId)
            .ExecuteDeleteAsync(ct);

        return deleted == 1;
    }
}