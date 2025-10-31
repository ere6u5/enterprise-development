using Xunit;
using CarRental.Domain.Data;
using CarRental.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Domain.Tests;

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
        _models = TestData.CarModels;
        _generations = TestData.ModelGenerations;
        _cars = TestData.Cars;
        _clients = TestData.Clients;
        _rentals = TestData.Rentals;
    }

    /// <summary>
    /// Тест получения клиентов, бравших указанную модель, отсортированных по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByCarModelSortedByName()
    {
        const string targetModel = "Toyota Camry";
        const int expectedCount = 3;
        const string expectedFirstName = "Иванов Иван Иванович";
        const string expectedSecondName = "Петров Петр Петрович";
        const string expectedThirdName = "Сидорова Мария Сергеевна";

        var clients = _rentals
            .Where(r => r.Car.ModelGeneration.Model.Name == targetModel)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedCount, clients.Count);
        Assert.Equal(expectedFirstName, clients[0].FullName);
        Assert.Equal(expectedSecondName, clients[1].FullName);
        Assert.Equal(expectedThirdName, clients[2].FullName);
    }

    /// <summary>
    /// Тест получения автомобилей, находящихся в аренде
    /// </summary>
    [Fact]
    public void GetCurrentlyRentedCars()
    {
        var testDate = new DateTime(2024, 1, 15, 12, 0, 0);
        const int expectedCount = 6;
        var expectedPlates = new[] { "A123BC", "C789FG", "F678LM", "J567RS", "L123VW", "N789ZA" };

        var rentedCars = _rentals
            .Where(r => r.RentalDate.AddHours(r.RentalHours) > testDate)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        Assert.Equal(expectedCount, rentedCars.Count);

        foreach (var expectedPlate in expectedPlates)
        {
            Assert.Contains(rentedCars, c => c.LicensePlate == expectedPlate);
        }
    }

    /// <summary>
    /// Тест получения топ 5 наиболее часто арендуемых автомобилей
    /// </summary>
    [Fact]
    public void GetTop5RentedCars()
    {
        const int expectedCount = 5;
        const string expectedTopCarPlate = "A123BC";
        const int expectedTopCarRentalCount = 3;

        var topCars = _rentals
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, RentalCount = g.Count() })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedCount, topCars.Count);
        Assert.Equal(expectedTopCarPlate, topCars[0].Car.LicensePlate);
        Assert.Equal(expectedTopCarRentalCount, topCars[0].RentalCount);
    }

    /// <summary>
    /// Тест получения количества аренд для каждого автомобиля
    /// </summary>
    [Fact]
    public void GetRentalCountPerCar()
    {
        const int expectedTotalCars = 15;
        const int expectedToyotaRentalCount = 3;
        const int expectedBmwRentalCount = 2;
        const int carIdWithThreeRentals = 1;
        const int carIdWithTwoRentals = 2;

        var carsWithRentalCount = _cars
            .Select(car => new
            {
                Car = car,
                RentalCount = _rentals.Count(r => r.CarId == car.Id)
            })
            .ToList();

        Assert.Equal(expectedTotalCars, carsWithRentalCount.Count);

        var toyota = carsWithRentalCount.First(c => c.Car.Id == carIdWithThreeRentals);
        var bmw = carsWithRentalCount.First(c => c.Car.Id == carIdWithTwoRentals);

        Assert.Equal(expectedToyotaRentalCount, toyota.RentalCount);
        Assert.Equal(expectedBmwRentalCount, bmw.RentalCount);
        Assert.True(carsWithRentalCount.All(x => x.RentalCount >= 0));
    }

    /// <summary>
    /// Тест получения топ 5 клиентов по сумме аренды
    /// </summary>
    [Fact]
    public void GetTop5ClientsByRentalSum()
    {
        const int expectedCount = 5;
        const string expectedTopClientName = "Петров Петр Петрович"; // Исправлено согласно актуальным данным

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

        Assert.Equal(expectedCount, topClients.Count);
        Assert.Equal(expectedTopClientName, topClients[0].Client.FullName);
    }
}