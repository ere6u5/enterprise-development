using CarRental.Data;
using CarRental.Data.Services;
using CarRental.Domain.Data;
using CarRental.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register repositories
var rentalRepository = new RentalRepository();
var carRepository = new CarRepository();
var clientRepository = new ClientRepository();

carRepository.SetRentalRepository(rentalRepository);
clientRepository.SetRentalRepository(rentalRepository);

builder.Services.AddSingleton<IRentalRepository>(rentalRepository);
builder.Services.AddSingleton<ICarRepository>(carRepository);
builder.Services.AddSingleton<IClientRepository>(clientRepository);

// Register analytics service
builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

InitializeTestData(carRepository, clientRepository, rentalRepository);

app.Run();

void InitializeTestData(ICarRepository carRepo, IClientRepository clientRepo, IRentalRepository rentalRepo)
{
    var models = TestData.GetCarModels();
    var generations = TestData.GetModelGenerations(models);
    var cars = TestData.GetCars(generations);
    var clients = TestData.GetClients();
    var rentals = TestData.GetRentals(cars, clients);

    foreach (var car in cars)
    {
        carRepo.Add(car);
    }

    foreach (var client in clients)
    {
        clientRepo.Add(client);
    }

    foreach (var rental in rentals)
    {
        rentalRepo.Add(rental);
    }

    Console.WriteLine($"Initialized: {carRepo.GetAll().Count()} cars, {clientRepo.GetAll().Count()} clients, {rentalRepo.GetAll().Count()} rentals");
}