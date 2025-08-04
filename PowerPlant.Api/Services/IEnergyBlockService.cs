using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;

namespace PowerPlant.Api.Services;

public interface IEnergyBlockService
{
    Task<IEnumerable<EnergyBlockResponse>> GetAllAsync(int stationId, CancellationToken ct);
    Task<EnergyBlockResponse?> GetByIdAsync(int stationId, int id, CancellationToken ct);
    Task<EnergyBlockResponse> CreateAsync(int stationId, CreateEnergyBlockRequest req, CancellationToken ct);
    Task<bool> UpdateAsync(int stationId, int id, UpdateEnergyBlockRequest req, CancellationToken ct);
    Task<bool> DeleteAsync(int stationId, int id, CancellationToken ct);
}