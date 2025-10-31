using CarRental.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarRental.Domain.Data;

/// <summary>
/// Генератор тестовых данных для проката автомобилей
/// </summary>
public static class TestData
{
    /// <summary>
    /// Список моделей автомобилей
    /// </summary>
    public static List<CarModel> CarModels { get; } = new()
    {
        new CarModel { Id = 1, Name = "Toyota Camry", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Business" },
        new CarModel { Id = 2, Name = "BMW X5", DriveType = "AWD", SeatsCount = 5, BodyType = "SUV", Class = "Premium" },
        new CarModel { Id = 3, Name = "Lada Vesta", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" },
        new CarModel { Id = 4, Name = "Kia Rio", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" },
        new CarModel { Id = 5, Name = "Hyundai Solaris", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" },
        new CarModel { Id = 6, Name = "Mercedes E-Class", DriveType = "RWD", SeatsCount = 5, BodyType = "Sedan", Class = "Premium" },
        new CarModel { Id = 7, Name = "Audi Q7", DriveType = "AWD", SeatsCount = 7, BodyType = "SUV", Class = "Premium" },
        new CarModel { Id = 8, Name = "Volkswagen Polo", DriveType = "FWD", SeatsCount = 5, BodyType = "Hatchback", Class = "Economy" },
        new CarModel { Id = 9, Name = "Skoda Octavia", DriveType = "FWD", SeatsCount = 5, BodyType = "Liftback", Class = "Business" },
        new CarModel { Id = 10, Name = "Ford Focus", DriveType = "FWD", SeatsCount = 5, BodyType = "Hatchback", Class = "Economy" },
        new CarModel { Id = 11, Name = "Nissan Qashqai", DriveType = "AWD", SeatsCount = 5, BodyType = "Crossover", Class = "Business" },
        new CarModel { Id = 12, Name = "Toyota RAV4", DriveType = "AWD", SeatsCount = 5, BodyType = "SUV", Class = "Business" },
        new CarModel { Id = 13, Name = "Hyundai Tucson", DriveType = "AWD", SeatsCount = 5, BodyType = "SUV", Class = "Business" },
        new CarModel { Id = 14, Name = "Kia Sportage", DriveType = "AWD", SeatsCount = 5, BodyType = "SUV", Class = "Business" },
        new CarModel { Id = 15, Name = "BMW 3 Series", DriveType = "RWD", SeatsCount = 5, BodyType = "Sedan", Class = "Premium" }
    };

    /// <summary>
    /// Список поколений моделей
    /// </summary>
    public static List<ModelGeneration> ModelGenerations { get; } = CreateModelGenerations();

    /// <summary>
    /// Список автомобилей
    /// </summary>
    public static List<Car> Cars { get; } = CreateCars();

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Client> Clients { get; } = new()
    {
        new Client { Id = 1, LicenseNumber = "LIC001", FullName = "Иванов Иван Иванович", BirthDate = new DateOnly(1990, 5, 15) },
        new Client { Id = 2, LicenseNumber = "LIC002", FullName = "Петров Петр Петрович", BirthDate = new DateOnly(1985, 8, 22) },
        new Client { Id = 3, LicenseNumber = "LIC003", FullName = "Сидорова Мария Сергеевна", BirthDate = new DateOnly(1995, 3, 10) },
        new Client { Id = 4, LicenseNumber = "LIC004", FullName = "Козлов Алексей Владимирович", BirthDate = new DateOnly(1988, 11, 5) },
        new Client { Id = 5, LicenseNumber = "LIC005", FullName = "Николаева Екатерина Дмитриевна", BirthDate = new DateOnly(1992, 7, 30) },
        new Client { Id = 6, LicenseNumber = "LIC006", FullName = "Васильев Дмитрий Сергеевич", BirthDate = new DateOnly(1987, 2, 14) },
        new Client { Id = 7, LicenseNumber = "LIC007", FullName = "Павлова Анна Викторовна", BirthDate = new DateOnly(1993, 9, 8) },
        new Client { Id = 8, LicenseNumber = "LIC008", FullName = "Семенов Артем Игоревич", BirthDate = new DateOnly(1984, 12, 3) },
        new Client { Id = 9, LicenseNumber = "LIC009", FullName = "Федорова Ольга Петровна", BirthDate = new DateOnly(1991, 6, 25) },
        new Client { Id = 10, LicenseNumber = "LIC010", FullName = "Морозов Сергей Александрович", BirthDate = new DateOnly(1989, 4, 17) },
        new Client { Id = 11, LicenseNumber = "LIC011", FullName = "Волкова Ирина Олеговна", BirthDate = new DateOnly(1994, 1, 12) },
        new Client { Id = 12, LicenseNumber = "LIC012", FullName = "Алексеев Максим Викторович", BirthDate = new DateOnly(1986, 10, 30) },
        new Client { Id = 13, LicenseNumber = "LIC013", FullName = "Лебедева Татьяна Михайловна", BirthDate = new DateOnly(1996, 7, 19) },
        new Client { Id = 14, LicenseNumber = "LIC014", FullName = "Новиков Андрей Николаевич", BirthDate = new DateOnly(1983, 3, 22) },
        new Client { Id = 15, LicenseNumber = "LIC015", FullName = "Кузнецова Светлана Анатольевна", BirthDate = new DateOnly(1997, 11, 7) }
    };

    /// <summary>
    /// Список аренд (фиксированные данные для тестирования)
    /// </summary>
    public static List<Rental> Rentals { get; } = CreateRentals();

    /// <summary>
    /// Создает список поколений моделей автомобилей с установленными связями с моделями
    /// </summary>
    /// <returns>Список поколений моделей с заполненными данными</returns>
    private static List<ModelGeneration> CreateModelGenerations()
    {
        var generations = new List<ModelGeneration>();

        for (int i = 1; i <= 15; i++)
        {
            var model = CarModels[i - 1];
            generations.Add(new ModelGeneration
            {
                Id = i,
                Year = 2020 + (i % 4) + 1,
                EngineVolume = 1.5 + (i * 0.1),
                Transmission = i % 3 == 0 ? "Manual" : "Automatic",
                RentalPricePerHour = 800 + (i * 100),
                ModelId = i,
                Model = model
            });
        }

        return generations;
    }

    /// <summary>
    /// Создает список автомобилей с установленными связями с поколениями моделей
    /// </summary>
    /// <returns>Список автомобилей с заполненными данными</returns>
    private static List<Car> CreateCars()
    {
        var cars = new List<Car>();
        var licensePlates = new[] { "A123BC", "B456DE", "C789FG", "D012HI", "E345JK", "F678LM", "G901NO", "H234PQ", "J567RS", "K890TU", "L123VW", "M456XY", "N789ZA", "P012BC", "R345DE" };
        var colors = new[] { "Red", "Blue", "White", "Black", "Silver", "Gray", "Black", "Blue", "White", "Red", "Green", "Silver", "Black", "White", "Blue" };

        for (int i = 1; i <= 15; i++)
        {
            var generation = ModelGenerations[i - 1];
            cars.Add(new Car
            {
                Id = i,
                LicensePlate = licensePlates[i - 1],
                Color = colors[i - 1],
                ModelGenerationId = i,
                ModelGeneration = generation
            });
        }

        return cars;
    }

    /// <summary>
    /// Создает список аренд с установленными связями с автомобилями и клиентами
    /// </summary>
    /// <returns>Список аренд с заполненными данными для тестирования</returns>
    private static List<Rental> CreateRentals()
    {
        var baseDate = new DateTime(2024, 1, 15, 12, 0, 0);
        var rentals = new List<Rental>();

        // Сначала создаем все объекты с правильными связями
        var rentalId = 1;

        // Toyota Camry - популярный автомобиль (3 аренды)
        AddRental(rentalId++, 1, 1, baseDate.AddDays(-1), 24, rentals);
        AddRental(rentalId++, 1, 2, baseDate.AddHours(-2), 48, rentals);
        AddRental(rentalId++, 1, 3, baseDate.AddDays(-7), 72, rentals);

        // BMW X5 (2 аренды)
        AddRental(rentalId++, 2, 1, baseDate.AddDays(-5), 24, rentals);
        AddRental(rentalId++, 2, 4, baseDate.AddDays(-3), 48, rentals);

        // Lada Vesta (2 аренды)
        AddRental(rentalId++, 3, 3, baseDate.AddHours(-1), 72, rentals);
        AddRental(rentalId++, 3, 5, baseDate.AddDays(-10), 24, rentals);

        // Volkswagen Polo (2 аренды)
        AddRental(rentalId++, 8, 9, baseDate.AddDays(-8), 48, rentals);
        AddRental(rentalId++, 8, 10, baseDate.AddDays(-12), 24, rentals);

        // Остальные автомобили по 1 аренде
        AddRental(rentalId++, 4, 4, baseDate.AddDays(-10), 24, rentals);
        AddRental(rentalId++, 5, 6, baseDate.AddDays(-2), 36, rentals);
        AddRental(rentalId++, 6, 7, baseDate.AddHours(-6), 96, rentals);
        AddRental(rentalId++, 7, 8, baseDate.AddDays(-15), 120, rentals);
        AddRental(rentalId++, 9, 11, baseDate.AddHours(-3), 60, rentals);
        AddRental(rentalId++, 10, 12, baseDate.AddDays(-4), 72, rentals);
        AddRental(rentalId++, 11, 13, baseDate.AddHours(-12), 84, rentals);
        AddRental(rentalId++, 12, 14, baseDate.AddDays(-6), 48, rentals);
        AddRental(rentalId++, 13, 15, baseDate.AddHours(-8), 96, rentals);
        AddRental(rentalId++, 14, 1, baseDate.AddDays(-20), 72, rentals);
        AddRental(rentalId++, 15, 2, baseDate.AddDays(-25), 120, rentals);

        return rentals;
    }

    /// <summary>
    /// Вспомогательный метод для добавления аренды в список
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <param name="carId">Идентификатор автомобиля</param>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="rentalDate">Дата и время начала аренды</param>
    /// <param name="rentalHours">Продолжительность аренды в часах</param>
    /// <param name="rentals">Список аренд для добавления</param>
    private static void AddRental(int id, int carId, int clientId, DateTime rentalDate, int rentalHours, List<Rental> rentals)
    {
        var car = Cars[carId - 1];
        var client = Clients[clientId - 1];

        rentals.Add(new Rental
        {
            Id = id,
            CarId = carId,
            ClientId = clientId,
            RentalDate = rentalDate,
            RentalHours = rentalHours,
            Car = car,
            Client = client
        });
    }
}