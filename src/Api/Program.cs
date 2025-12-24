using Domain.Entities;
using Domain.Repositories;
using Application.Service;
using Infrastructure.Db.Repositories;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Nats;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://*:5000;https://*:5001");

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
builder.Services.AddScoped<IRepository<CarModel>, DbCarModelRepository>();
builder.Services.AddScoped<IRepository<ModelGeneration>, DbModelGenerationRepository>();
builder.Services.AddScoped<IRepository<Car>, DbCarRepository>();
builder.Services.AddScoped<IRepository<Client>, DbClientRepository>();
builder.Services.AddScoped<IRepository<Rental>, DbRentalRepository>();

builder.Services.AddScoped<INatsService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<NatsService>>();
    var natsUrl = builder.Configuration["Nats:Url"] ?? "nats://localhost:4222";
    return new NatsService(natsUrl, logger);
});

// Регистрируем сервисы
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<IModelGenerationService, ModelGenerationService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRentalService>(provider =>
{
    var rentalRepository = provider.GetRequiredService<IRepository<Domain.Entities.Rental>>();
    var carRepository = provider.GetRequiredService<IRepository<Domain.Entities.Car>>();
    var clientRepository = provider.GetRequiredService<IRepository<Domain.Entities.Client>>();
    var carModelRepository = provider.GetRequiredService<IRepository<Domain.Entities.CarModel>>();
    var modelGenerationRepository = provider.GetRequiredService<IRepository<Domain.Entities.ModelGeneration>>();
    var natsService = provider.GetRequiredService<INatsService>();
    
    return new RentalService(
        rentalRepository,
        carRepository,
        clientRepository,
        carModelRepository,
        modelGenerationRepository,
        natsService);
});

builder.Services.AddControllers();
//builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
//app.MapGrpcService<Api.Grpc.RentalGeneratorService>();

app.Run();