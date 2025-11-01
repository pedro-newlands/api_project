using api_project.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjetoPokeShop.Data;
using ProjetoPokeShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IManagementService, ManagementService>();
builder.Services.AddScoped<ICenterService, CenterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
