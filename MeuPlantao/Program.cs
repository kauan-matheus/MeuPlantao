using Microsoft.EntityFrameworkCore;
using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Infrastructure.Repository;
using MeuPlantao.Application.Services;
using MeuPlantao.Application.Services.Auth;
using MeuPlantao.Application.Services.Plantao;
using MeuPlantao.Application.Services.Profissional;
using MeuPlantao.Application.Services.Setor;
using MeuPlantao.Application.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Application.Services.TrocaHistorico;
using MeuPlantao.Application.Services.TrocaPlantao;
using System.IdentityModel.Tokens.Jwt;
using MeuPlantao.Application.Services.PlantaoHistorico;
using MeuPlantao.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using MeuPlantao.Application.Validators;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPlantaoRepository, PlantaoRepository>();
builder.Services.AddScoped<ITrocaRepository, TrocaRepository>();
builder.Services.AddScoped<IProfRepository, ProfRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPlantaoService, PlantaoService>();
builder.Services.AddScoped<IPlantaoHistoricoService, PlantaoHistoricoService>();
builder.Services.AddScoped<IProfissionalService, ProfissionalService>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<ITrocaHistoricoService, TrocaHistoricoService>();
builder.Services.AddScoped<ITrocaPlantaoService, TrocaPlantaoService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddValidatorsFromAssembly(typeof(AuthLoginValidator).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

Console.WriteLine(builder.Configuration.GetConnectionString("Default"));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();