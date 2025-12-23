using Domain.Repositories;
using Application.Service;
using Infrastructure.InMemory.Repositories;
using Domain.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var seeder = new DataSeeder();

builder.Services.AddScoped<IRepository<Domain.Entities.CarModel>, InMemoryCarModelRepository>(_ => new InMemoryCarModelRepository(seeder));
builder.Services.AddScoped<IRepository<Domain.Entities.ModelGeneration>, InMemoryModelGenerationRepository>(_ => new InMemoryModelGenerationRepository(seeder));
builder.Services.AddScoped<IRepository<Domain.Entities.Car>, InMemoryCarRepository>(_ => new InMemoryCarRepository(seeder));
builder.Services.AddScoped<IRepository<Domain.Entities.Client>, InMemoryClientRepository>(_ => new InMemoryClientRepository(seeder));
builder.Services.AddScoped<IRepository<Domain.Entities.Rental>, InMemoryRentalRepository>(_ => new InMemoryRentalRepository(seeder));

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
app.UseAuthorization();
app.MapControllers();

app.Run();