using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Dtos.Responses;

namespace PowerPlant.Api.Services;

public interface IStationService
{
    Task<IEnumerable<StationResponse>> GetAllAsync(CancellationToken ct);
    Task<StationResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<StationResponse> CreateAsync(CreateStationRequest req, CancellationToken ct);
    Task<bool> UpdateAsyncNewApproach(int id, UpdateStationRequest req, CancellationToken ct);
    Task<bool> UpdateAsyncClassicApproach(int id, UpdateStationRequest req, CancellationToken ct);
    Task<bool> DeleteAsyncNewApproach(int id, CancellationToken ct);
    Task<bool> DeleteAsyncClassicApproach(int id, CancellationToken ct);
}