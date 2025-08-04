using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PowerPlant.Api.Data;
using PowerPlant.Api.Filters;
using PowerPlant.Api.Mapping;
using PowerPlant.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// БД
builder.Services.AddDbContext<PowerPlantContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Local")));

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer            = false,
            ValidateAudience          = false,
            ValidateLifetime          = true,
            ValidateIssuerSigningKey  = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("Admin", p => p.RequireRole("Admin"));
    opts.AddPolicy("User",  p => p.RequireRole("User"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "PowerPlant API", Version = "v1" });

    var jwtScheme = new OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.Http,
        Scheme       = "bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "Введите **только** JWT‑токен (без prefix)."
    };

    opt.AddSecurityDefinition("Bearer", jwtScheme);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

// Services
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IEnergyBlockService, EnergyBlockService>();

// Filters
builder.Services.AddScoped<StationExistsAttribute>();

// Automapper
builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();