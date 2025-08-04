using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PowerPlant.Api.Data;
using PowerPlant.Api.Dtos.Requests;
using PowerPlant.Api.Services;

namespace PowerPlant.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService service, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate(LoginRequest request, CancellationToken ct)
    {
        var token = await service.AuthenticateAsync(request, ct);
        
        if (token == null)
            return Unauthorized();
        
        return Ok(new { access_token = token });
    }
}