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
    /// Получить список моделей автомобилей
    /// </summary>
    public static List<CarModel> GetCarModels()
    {
        return new List<CarModel>
        {
            new CarModel { Id = 1, Name = "Toyota Camry", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Business" },
            new CarModel { Id = 2, Name = "BMW X5", DriveType = "AWD", SeatsCount = 5, BodyType = "SUV", Class = "Premium" },
            new CarModel { Id = 3, Name = "Lada Vesta", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" },
            new CarModel { Id = 4, Name = "Kia Rio", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" },
            new CarModel { Id = 5, Name = "Hyundai Solaris", DriveType = "FWD", SeatsCount = 5, BodyType = "Sedan", Class = "Economy" }
        };
    }

    /// <summary>
    /// Получить список поколений моделей
    /// </summary>
    public static List<ModelGeneration> GetModelGenerations(List<CarModel> models)
    {
        return new List<ModelGeneration>
        {
            new ModelGeneration { Id = 1, Year = 2022, EngineVolume = 2.5, Transmission = "Automatic", RentalPricePerHour = 1500, Model = models[0] },
            new ModelGeneration { Id = 2, Year = 2023, EngineVolume = 3.0, Transmission = "Automatic", RentalPricePerHour = 2500, Model = models[1] },
            new ModelGeneration { Id = 3, Year = 2021, EngineVolume = 1.6, Transmission = "Manual", RentalPricePerHour = 800, Model = models[2] },
            new ModelGeneration { Id = 4, Year = 2022, EngineVolume = 1.6, Transmission = "Automatic", RentalPricePerHour = 1000, Model = models[3] },
            new ModelGeneration { Id = 5, Year = 2023, EngineVolume = 1.4, Transmission = "Automatic", RentalPricePerHour = 1100, Model = models[4] }
        };
    }

    /// <summary>
    /// Получить список автомобилей
    /// </summary>
    public static List<Car> GetCars(List<ModelGeneration> generations)
    {
        return new List<Car>
        {
            new Car { Id = 1, LicensePlate = "A123BC", Color = "Red", ModelGeneration = generations[0] },
            new Car { Id = 2, LicensePlate = "B456DE", Color = "Blue", ModelGeneration = generations[1] },
            new Car { Id = 3, LicensePlate = "C789FG", Color = "White", ModelGeneration = generations[2] },
            new Car { Id = 4, LicensePlate = "D012HI", Color = "Black", ModelGeneration = generations[3] },
            new Car { Id = 5, LicensePlate = "E345JK", Color = "Silver", ModelGeneration = generations[4] }
        };
    }

    /// <summary>
    /// Получить список клиентов
    /// </summary>
    public static List<Client> GetClients()
    {
        return new List<Client>
        {
            new Client { Id = 1, LicenseNumber = "LIC001", FullName = "Иванов Иван Иванович", BirthDate = new DateTime(1990, 5, 15) },
            new Client { Id = 2, LicenseNumber = "LIC002", FullName = "Петров Петр Петрович", BirthDate = new DateTime(1985, 8, 22) },
            new Client { Id = 3, LicenseNumber = "LIC003", FullName = "Сидорова Мария Сергеевна", BirthDate = new DateTime(1995, 3, 10) },
            new Client { Id = 4, LicenseNumber = "LIC004", FullName = "Козлов Алексей Владимирович", BirthDate = new DateTime(1988, 11, 5) },
            new Client { Id = 5, LicenseNumber = "LIC005", FullName = "Николаева Екатерина Дмитриевна", BirthDate = new DateTime(1992, 7, 30) }
        };
    }

    /// <summary>
    /// Получить список аренд (фиксированные данные для тестирования)
    /// </summary>
    public static List<Rental> GetRentals(List<Car> cars, List<Client> clients)
    {
        var now = DateTime.Now;
        var rentals = new List<Rental>();

        rentals.Add(new Rental
        {
            Id = 1,
            CarId = 1,  // Toyota Camry
            ClientId = 1, // Иванов
            Car = cars[0],
            Client = clients[0],
            RentalDate = now.AddDays(-1), // Вчера
            RentalHours = 24  // Заканчивается сейчас
        });

        rentals.Add(new Rental
        {
            Id = 2,
            CarId = 1,  // Toyota Camry  
            ClientId = 2, // Петров
            Car = cars[0],
            Client = clients[1],
            RentalDate = now.AddHours(-2), // 2 часа назад
            RentalHours = 48  // Еще 46 часов в аренде
        });

        rentals.Add(new Rental
        {
            Id = 3,
            CarId = 2,  // BMW X5
            ClientId = 1, // Иванов
            Car = cars[1],
            Client = clients[0],
            RentalDate = now.AddDays(-5), // 5 дней назад
            RentalHours = 24  // Уже закончилась
        });

        rentals.Add(new Rental
        {
            Id = 4,
            CarId = 3,  // Lada Vesta
            ClientId = 3, // Сидорова
            Car = cars[2],
            Client = clients[2],
            RentalDate = now.AddHours(-1), // 1 час назад
            RentalHours = 72  // Еще 71 час в аренде
        });

        rentals.Add(new Rental
        {
            Id = 5,
            CarId = 4,  // Kia Rio
            ClientId = 4, // Козлов
            Car = cars[3],
            Client = clients[3],
            RentalDate = now.AddDays(-10), // 10 дней назад
            RentalHours = 24  // Уже закончилась
        });

        return rentals;
    }
}