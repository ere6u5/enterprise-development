using CarRental.Domain.Model;
using CarRental.Infrastructure.DataSeed;

namespace CarRental.Tests.Fixture;

public class TestFixture
{
    public List<CarModel> CarModels { get; }
    public List<ModelGeneration> ModelGenerations { get; }
    public List<Car> Cars { get; }
    public List<Customer> Customers { get; }
    public List<Rental> Rentals { get; }

    public TestFixture()
    {
        CarModels = CarRentalSeeder.GetCarModels();
        ModelGenerations = CarRentalSeeder.GetModelGenerations();
        Cars = CarRentalSeeder.GetCars();
        Customers = CarRentalSeeder.GetCustomers();
        Rentals = CarRentalSeeder.GetRentals();

        // Проверка что данные корректны
        ValidateTestData();
    }

    private void ValidateTestData()
    {
        // Проверяем что есть аренды
        if (Rentals.Count == 0)
            throw new InvalidOperationException("Нет данных об арендах");

        // Проверяем что есть клиенты с арендами
        var customersWithRentals = Rentals.Select(r => r.CustomerId).Distinct().Count();
        if (customersWithRentals == 0)
            throw new InvalidOperationException("Нет клиентов с арендами");

        // Проверяем что есть автомобили с арендами
        var carsWithRentals = Rentals.Select(r => r.CarId).Distinct().Count();
        if (carsWithRentals == 0)
            throw new InvalidOperationException("Нет автомобилей с арендами");
    }
}