using System.Threading;
using System.Threading.Tasks;
using PowerPlant.Api.Dtos.Requests;

namespace PowerPlant.Api.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(LoginRequest request, CancellationToken ct);
}