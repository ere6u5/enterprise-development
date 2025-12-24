namespace Tests;

/// <summary>
/// Unit tests for car rental analytical queries
/// </summary>
/// <param name="fixture">Test fixture with test data</param>
public class CarRentalTests(CarRentalFixture fixture) : IClassFixture<CarRentalFixture>
{
    /// <summary>
    /// Test: Вывести информацию обо всех клиентах, которые брали в аренду автомобили указанной модели, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByModelOrderedByFullName()
    {
        // Arrange
        var modelId = 1; // Toyota Camry
        var expectedClientNames = new List<string>
        {
            "Ivanov Ivan Ivanovich",
            "Petrov Petr Petrovich"
        };

        // Act
        var clients = fixture.Rentals
            .Where(r => r.Car.ModelGeneration.Model.Id == modelId)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.Equal(2, clients.Count);
        Assert.Equal(expectedClientNames, clients.Select(c => c.FullName));
    }

    /// <summary>
    /// Test: Вывести информацию об автомобилях, находящихся в аренде
    /// </summary>
    [Fact]
    public void GetRentedCars()
    {
        // Arrange
        // Используем время, которое точно находится в периоде аренды
        var currentTime = new DateTime(2024, 1, 15, 12, 0, 0); // 12:00 15 января - во время первой аренды

        // Act
        var rentedCars = fixture.Rentals
            .Where(r => r.RentalStart <= currentTime && r.RentalStart.AddHours(r.RentalHours) >= currentTime)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        // Assert
        Assert.NotEmpty(rentedCars);
        Assert.Contains(rentedCars, c => c.Id == 1); // Автомобиль с ID 1 должен быть в аренде
    }

    /// <summary>
    /// Test: Вывести топ 5 наиболее часто арендуемых автомобилей
    /// </summary>
    [Fact]
    public void GetTop5MostRentedCars()
    {
        // Act
        var topCars = fixture.Rentals
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        // Assert
        Assert.Equal(5, topCars.Count);
        
        // В тестовых данных все автомобили с ID 1-5 арендованы по 2 раза
        Assert.All(topCars, x => Assert.Equal(2, x.Count));
    }

    /// <summary>
    /// Test: Для каждого автомобиля вывести число аренд
    /// </summary>
    [Fact]
    public void GetRentalCountPerCar()
    {
        // Arrange
        var carIds = fixture.Cars.Select(c => c.Id).ToList();

        // Act
        var rentalCounts = fixture.Rentals
            .GroupBy(r => r.CarId)
            .ToDictionary(g => g.Key, g => g.Count());

        // Assert
        // Проверяем только те автомобили, которые были в аренде
        // В тестовых данных автомобили 1-5 арендованы по 2 раза, 6-7 не арендованы
        for (var carId = 1; carId <= 5; carId++)
        {
            Assert.True(rentalCounts.ContainsKey(carId), $"Car {carId} should have rental count");
            Assert.Equal(2, rentalCounts[carId]);
        }
        
        // Автомобили 6 и 7 не должны быть в списке аренд
        for (var carId = 6; carId <= 7; carId++)
        {
            Assert.False(rentalCounts.ContainsKey(carId), $"Car {carId} should not have rental count");
        }
    }

    /// <summary>
    /// Test: Вывести топ 5 клиентов по сумме аренды
    /// </summary>
    [Fact]
    public void GetTop5ClientsByRentalSum()
    {
        // Act
        var clientSums = fixture.Rentals
            .GroupBy(r => r.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                TotalCost = g.Sum(r => r.Car.ModelGeneration.RentalCostPerHour * r.RentalHours)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();

        // Assert
        Assert.Equal(5, clientSums.Count);
        Assert.All(clientSums, x => Assert.True(x.TotalCost > 0));
    }
}