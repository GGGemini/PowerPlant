using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PowerPlant.Api.Data;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;
using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Services;

public class StationService(PowerPlantContext db, IMapper mapper) : IStationService
{
    public async Task<IEnumerable<StationResponse>> GetAllAsync(CancellationToken ct) =>
        await db.Stations
            .ProjectTo<StationResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

    public async Task<StationResponse?> GetByIdAsync(int id, CancellationToken ct) =>
        await db.Stations
            .Where(x => x.Id == id)
            .ProjectTo<StationResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    
    public async Task<StationResponse> CreateAsync(CreateStationRequest req, CancellationToken ct)
    {
        var entity = mapper.Map<Station>(req);
        
        db.Stations.Add(entity);
        await db.SaveChangesAsync(ct);

        return mapper.Map<StationResponse>(entity);
    }
    
    public async Task<bool> UpdateAsync(int id, UpdateStationRequest req, CancellationToken ct)
    {
        var updated = await db.Stations
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(s => s.Name, req.Name), ct);
        
        return updated == 1;
    }
    
    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await db.Stations
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);
        
        return deleted == 1;
    }
}