using Domain.Repositories;
using Application.Service;
using Infrastructure.Db.Repositories;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Получаем строку подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Регистрируем DbContext с MySQL
builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Регистрируем Db-репозитории
builder.Services.AddScoped<IRepository<Domain.Entities.CarModel>, DbCarModelRepository>();
builder.Services.AddScoped<IRepository<Domain.Entities.ModelGeneration>, DbModelGenerationRepository>();
builder.Services.AddScoped<IRepository<Domain.Entities.Car>, DbCarRepository>();
builder.Services.AddScoped<IRepository<Domain.Entities.Client>, DbClientRepository>();
builder.Services.AddScoped<IRepository<Domain.Entities.Rental>, DbRentalRepository>();

// Регистрируем сервисы
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<IModelGenerationService, ModelGenerationService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();