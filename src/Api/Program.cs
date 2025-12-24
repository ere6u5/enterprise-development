using Domain.Entities;
using Domain.Repositories;
using Application.Service;
using Infrastructure.Db.Repositories;
using Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Nats;

using Api;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://*:5000;https://*:5001");
builder.Services.AddSwaggerGen(options =>
{
    var xmlApiPath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
    var xmlApplicationPath = Path.Combine(AppContext.BaseDirectory, "Application.xml");
    var xmlDomainPath = Path.Combine(AppContext.BaseDirectory, "Domain.xml");
    
    if (File.Exists(xmlApiPath))
        options.IncludeXmlComments(xmlApiPath);
    
    if (File.Exists(xmlApplicationPath))
        options.IncludeXmlComments(xmlApplicationPath);
    
    if (File.Exists(xmlDomainPath))
        options.IncludeXmlComments(xmlDomainPath);
    
    options.SupportNonNullableReferenceTypes();
    options.UseAllOfToExtendReferenceSchemas();
});

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

// Регистрируем NatsService
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

// Упрощаем регистрацию RentalService
builder.Services.AddScoped<IRentalService, RentalService>();

// Регистрируем обработчик NATS сообщений
builder.Services.AddHostedService<NatsMessageHandlerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Применить миграции автоматически
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();
        
        // Ждем пока база данных станет доступной
        var maxRetries = 10;
        for (var i = 0; i < maxRetries; i++)
        {
            try
            {
                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("Database is available. Applying migrations...");
                    dbContext.Database.Migrate();
                    
                    // Заполнить начальными данными
                    DatabaseSeeder.Seed(dbContext);
                    Console.WriteLine("Database migrated and seeded successfully");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database not ready yet ({i + 1}/{maxRetries}): {ex.Message}");
                if (i == maxRetries - 1) throw;
                Thread.Sleep(3000); // Ждем 3 секунды перед следующей попыткой
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error migrating database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();