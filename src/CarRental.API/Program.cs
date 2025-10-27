using CarRental.Data;
using CarRental.Domain.Data;
using CarRental.Domain.Models;

/// <summary>
/// Точка входа приложения CarRental API
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var rentalRepository = new RentalRepository();
var carRepository = new CarRepository();
var clientRepository = new ClientRepository();

carRepository.SetRentalRepository(rentalRepository);
clientRepository.SetRentalRepository(rentalRepository);

builder.Services.AddSingleton<IRentalRepository>(rentalRepository);
builder.Services.AddSingleton<ICarRepository>(carRepository);
builder.Services.AddSingleton<IClientRepository>(clientRepository);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


InitializeTestData(carRepository, clientRepository, rentalRepository);

app.Run();

/// <summary>
/// Инициализировать тестовые данные в репозиториях
/// </summary>
/// <param name="carRepo">Репозиторий автомобилей</param>
/// <param name="clientRepo">Репозиторий клиентов</param>
/// <param name="rentalRepo">Репозиторий аренд</param>
void InitializeTestData(ICarRepository carRepo, IClientRepository clientRepo, IRentalRepository rentalRepo)
{
    // Используем тестовые данные из первой лабораторной
    var models = TestData.GetCarModels();
    var generations = TestData.GetModelGenerations(models);
    var cars = TestData.GetCars(generations);
    var clients = TestData.GetClients();
    var rentals = TestData.GetRentals(cars, clients);

    // Добавляем данные в репозитории
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