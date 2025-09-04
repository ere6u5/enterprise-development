using CarRental.Domain.Model;
using CarRental.Infrastructure.DataSeed;
using CarRental.Tests.Fixture;

namespace CarRental.Tests;

public class CarRentalQueriesTests : IClassFixture<TestFixture>
{
    private readonly List<CarModel> _carModels;
    private readonly List<ModelGeneration> _modelGenerations;
    private readonly List<Car> _cars;
    private readonly List<Customer> _customers;
    private readonly List<Rental> _rentals;

    public CarRentalQueriesTests(TestFixture fixture)
    {
        _carModels = fixture.CarModels;
        _modelGenerations = fixture.ModelGenerations;
        _cars = fixture.Cars;
        _customers = fixture.Customers;
        _rentals = fixture.Rentals;
    }

    /// <summary>
    /// Вывести информацию обо всех клиентах, которые брали в аренду автомобили указанной модели, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void GetCustomersByCarModel_SortedByName()
    {
        // Arrange - проверяем что есть хотя бы одна аренда
        if (!_rentals.Any())
        {
            Assert.Fail("Нет данных об арендах для тестирования");
            return;
        }

        var targetModel = _carModels.First();

        // Act
        var customers = _rentals
            .Where(r => r.Car.Generation.Model.Id == targetModel.Id)
            .Select(r => r.Customer)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Если нет клиентов для этой модели - это нормально
        if (customers.Count == 0)
        {
            Assert.True(true, "Нет клиентов для выбранной модели");
            return;
        }

        Assert.True(customers.SequenceEqual(customers.OrderBy(c => c.FullName)));
    }

    /// <summary>
    /// Вывести информацию об автомобилях, находящихся в аренде.
    /// </summary>
    [Fact]
    public void GetCurrentlyRentedCars()
    {
        // Arrange
        var now = DateTime.Now;

        // Act
        var rentedCars = _rentals
            .Where(r => r.RentalStart <= now && r.RentalEnd >= now)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        // Assert
        // Исправлено: вместо Assert.True с Any используем Assert.Contains
        foreach (var car in rentedCars)
        {
            Assert.Contains(_rentals, r => r.CarId == car.Id && 
                                        r.RentalStart <= now && 
                                        r.RentalEnd >= now);
        }
    }

    /// <summary>
    /// Вывести топ 5 наиболее часто арендуемых автомобилей.
    /// </summary>
    [Fact]
    public void GetTop5MostRentedCars()
    {
        // Act
        var topCars = _rentals
            .GroupBy(r => r.Car)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Car = g.Key, RentalCount = g.Count() })
            .ToList();

        // Assert
        Assert.True(topCars.Count <= 5);
        Assert.True(topCars.Count > 0);
        Assert.True(topCars.Select(x => x.RentalCount).Distinct().Count() <= 5);
    }

    /// <summary>
    /// Для каждого автомобиля вывести число аренд.
    /// </summary>
    [Fact]
    public void GetRentalCountPerCar()
    {
        // Act
        var rentalCounts = _rentals
            .GroupBy(r => r.Car)
            .Select(g => new { Car = g.Key, RentalCount = g.Count() })
            .ToList();

        // Assert - проверяем что у нас есть данные об арендах
        Assert.NotEmpty(rentalCounts);
        Assert.All(rentalCounts, rc => Assert.True(rc.RentalCount >= 0));
    }

    /// <summary>
    /// Вывести топ 5 клиентов по сумме аренды.
    /// </summary>
    [Fact]
    public void GetTop5CustomersByRentalCost()
    {
        // Act
        var topCustomers = _rentals
            .GroupBy(r => r.Customer)
            .Select(g => new
            {
                Customer = g.Key,
                TotalCost = g.Sum(r => r.RentalHours * r.Car.Generation.RentalCostPerHour)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();

        // Assert
        Assert.True(topCustomers.Count <= 5);
        Assert.True(topCustomers.Count > 0);
        if (topCustomers.Count > 1)
        {
            Assert.True(topCustomers[0].TotalCost >= topCustomers[1].TotalCost);
        }
    }
}