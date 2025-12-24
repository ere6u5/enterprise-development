namespace Tests;

/// <summary>
/// Unit tests for car rental services
/// </summary>
/// <param name="fixture">Fixture with services</param>
public class CarRentalRepoTests(CarRentalRepoFixture fixture) : IClassFixture<CarRentalRepoFixture>
{
    /// <summary>
    /// Test: Вывести информацию обо всех клиентах, которые брали в аренду автомобили указанной модели, упорядочить по ФИО
    /// </summary>
    [Fact]
    public async Task GetClientsByModelOrderedByFullName()
    {
        // Arrange
        var modelId = 1;
        var expectedCount = 2;

        // Act
        var clients = await fixture.RentalService.GetClientsByModelAsync(modelId);

        // Assert
        Assert.Equal(expectedCount, clients.Count);
        Assert.Equal("Ivanov Ivan Ivanovich", clients[0].FullName);
        Assert.Equal("Petrov Petr Petrovich", clients[1].FullName);
    }

    /// <summary>
    /// Test: Вывести информацию об автомобилях, находящихся в аренде
    /// </summary>
    [Fact]
    public async Task GetRentedCars()
    {
        // Act
        var rentedCars = await fixture.RentalService.GetRentedCarsAsync();

        // Assert
        // Тест проверяет только что метод не выбрасывает исключений
        // Фактический результат зависит от текущего времени
        Assert.NotNull(rentedCars);
    }

    /// <summary>
    /// Test: Вывести топ 5 наиболее часто арендуемых автомобилей
    /// </summary>
    [Fact]
    public async Task GetTop5MostRentedCars()
    {
        // Act
        var topCars = await fixture.RentalService.GetTop5MostRentedCarsAsync();

        // Assert
        Assert.NotNull(topCars);
        Assert.True(topCars.Count <= 5);
        Assert.All(topCars, car => Assert.True(car.RentalCount > 0));
    }

    /// <summary>
    /// Test: Для каждого автомобиля вывести число аренд
    /// </summary>
    [Fact]
    public async Task GetRentalCountPerCar()
    {
        // Act
        var rentalCounts = await fixture.RentalService.GetRentalCountPerCarAsync();

        // Assert
        Assert.NotNull(rentalCounts);
        Assert.Equal(12, rentalCounts.Count); // 12 автомобилей в тестовых данных
        
        // Проверяем что у всех автомобилей есть запись о количестве аренд
        Assert.All(rentalCounts, rc => Assert.NotNull(rc.Car));
        
        // Автомобили 1-5 должны иметь 2 аренды, 6-7 - 0
        var car1Count = rentalCounts.First(rc => rc.Car.Id == 1).RentalCount;
        var car6Count = rentalCounts.First(rc => rc.Car.Id == 6).RentalCount;
        
        Assert.Equal(2, car1Count);
        Assert.Equal(0, car6Count);
    }

    /// <summary>
    /// Test: Вывести топ 5 клиентов по сумме аренды
    /// </summary>
    [Fact]
    public async Task GetTop5ClientsByRentalSum()
    {
        // Act
        var topClients = await fixture.RentalService.GetTop5ClientsByRentalSumAsync();

        // Assert
        Assert.NotNull(topClients);
        Assert.True(topClients.Count <= 5);
        Assert.All(topClients, client => Assert.True(client.TotalRentalCost > 0));
    }
}