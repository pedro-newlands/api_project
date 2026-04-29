using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Filters;
using ProjetoPokeShop.Repositories;
using ProjetoPokeShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<CenterRepository>();
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<StorageRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICenterService, CenterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<AdminOnlyFilter>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokeShop API", Version = "v1" });

    c.AddSecurityDefinition("AdminPassword", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-Super-Password",
        Type = SecuritySchemeType.ApiKey,
        Description = "Digite a senha de administrador para acessar este endpoint"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AdminPassword"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
