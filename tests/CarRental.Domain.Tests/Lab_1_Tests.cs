using Xunit;
using CarRental.Domain.Data;
using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Тесты LINQ запросов для системы проката автомобилей
/// </summary>
public class CarRentalTests
{
    private readonly List<CarModel> _models;
    private readonly List<ModelGeneration> _generations;
    private readonly List<Car> _cars;
    private readonly List<Client> _clients;
    private readonly List<Rental> _rentals;

    /// <summary>
    /// Инициализация тестовых данных
    /// </summary>
    public CarRentalTests()
    {
        _models = TestData.GetCarModels();
        _generations = TestData.GetModelGenerations(_models);
        _cars = TestData.GetCars(_generations);
        _clients = TestData.GetClients();
        _rentals = TestData.GetRentals(_cars, _clients);
    }

    /// <summary>
    /// Тест получения клиентов, бравших указанную модель, отсортированных по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByCarModelSortedByName()
    {
        string targetModel = "Toyota Camry";

        var clients = _rentals
            .Where(r => r.Car.ModelGeneration.Model.Name == targetModel)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        var expectedClients = _clients
            .Where(c => _rentals.Any(r =>
                r.ClientId == c.Id &&
                r.Car.ModelGeneration.Model.Name == targetModel))
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(2, clients.Count);
        Assert.Equal("Иванов Иван Иванович", clients[0].FullName);
        Assert.Equal("Петров Петр Петрович", clients[1].FullName);
        Assert.Equal(expectedClients, clients);
    }

    /// <summary>
    /// Тест получения автомобилей, находящихся в аренде
    /// </summary>
    [Fact]
    public void GetCurrentlyRentedCars()
    {
        var now = DateTime.Now;

        var rentedCars = _rentals
            .Where(r => r.RentalDate.AddHours(r.RentalHours) > now)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        var expectedRentedCars = _cars
            .Where(c => _rentals.Any(r =>
                r.CarId == c.Id &&
                r.RentalDate.AddHours(r.RentalHours) > now))
            .Distinct()
            .ToList();

        Assert.Equal(2, rentedCars.Count);
        Assert.Contains(rentedCars, c => c.LicensePlate == "A123BC"); // Toyota Camry
        Assert.Contains(rentedCars, c => c.LicensePlate == "C789FG"); // Lada Vesta
        Assert.Equal(expectedRentedCars.Count, rentedCars.Count);
    }

    /// <summary>
    /// Тест получения топ 5 наиболее часто арендуемых автомобилей
    /// </summary>
    [Fact]
    public void GetTop5RentedCars()
    {
        var topCars = _rentals
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, RentalCount = g.Count() })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        Assert.Equal(4, topCars.Count);

        Assert.Equal("A123BC", topCars[0].Car.LicensePlate);
        Assert.Equal(2, topCars[0].RentalCount);

        for (int i = 0; i < topCars.Count - 1; i++)
        {
            Assert.True(topCars[i].RentalCount >= topCars[i + 1].RentalCount);
        }
    }

    /// <summary>
    /// Тест получения количества аренд для каждого автомобиля
    /// </summary>
    [Fact]
    public void GetRentalCountPerCar()
    {
        var carsWithRentalCount = _cars
            .Select(car => new
            {
                Car = car,
                RentalCount = _rentals.Count(r => r.CarId == car.Id)
            })
            .ToList();

        Assert.Equal(_cars.Count, carsWithRentalCount.Count);

        var toyota = carsWithRentalCount.First(c => c.Car.LicensePlate == "A123BC");
        Assert.Equal(2, toyota.RentalCount); // Toyota Camry - 2 аренды

        var bmw = carsWithRentalCount.First(c => c.Car.LicensePlate == "B456DE");
        Assert.Equal(1, bmw.RentalCount); // BMW X5 - 1 аренда

        Assert.True(carsWithRentalCount.All(x => x.RentalCount >= 0));
    }

    /// <summary>
    /// Тест получения топ 5 клиентов по сумме аренды
    /// </summary>
    [Fact]
    public void GetTop5ClientsByRentalSum()
    {
        var topClients = _rentals
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalAmount = g.Sum(r => r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour)
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(5)
            .ToList();

        Assert.Equal(4, topClients.Count);

        for (int i = 0; i < topClients.Count - 1; i++)
        {
            Assert.True(topClients[i].TotalAmount >= topClients[i + 1].TotalAmount);
        }

        Assert.Equal("Иванов Иван Иванович", topClients[0].Client.FullName);
    }
}