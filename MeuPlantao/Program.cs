using Microsoft.EntityFrameworkCore;
using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Interfaces;
using MeuPlantao.Infrastructure.Repository;
using MeuPlantao.Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MeuPlantao.Application.Services.Profissional;
using MeuPlantao.Application.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MeuPlantao.Application.Services.Setor;
using MeuPlantao.Application.Services.Plantao;
using MeuPlantao.Application.Services.Auth;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddScoped<ProfissionalService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<SetorService>();
        builder.Services.AddScoped<PlantaoService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<TokenService>();

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

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                // Tipo de autenticação HTTP
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                // Define o esquema como Bearer
                Scheme = "Bearer",
                // Indica que o formato do token é JWT
                BearerFormat = "JWT",
                // Diz que o token será enviado no header da requisição
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Insira o token JWT"
            });
        });

        var connectionString = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

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
    }
}