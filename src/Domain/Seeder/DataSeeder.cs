using Domain.Entities;
using Domain.Enums;

namespace Domain.Seeder;

/// <summary>
/// Class with data about entities
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// List of all car models
    /// </summary>
    public List<CarModel> CarModels =>
    [
        new()
        {
            Id = 1,
            Name = "Toyota Camry",
            DriveType = CarDriveType.FrontWheelDrive,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            CarClass = CarClass.Comfort
        },
        new()
        {
            Id = 2,
            Name = "BMW X5",
            DriveType = CarDriveType.AllWheelDrive,
            SeatCount = 5,
            BodyType = BodyType.SUV,
            CarClass = CarClass.Premium
        },
        new()
        {
            Id = 3,
            Name = "Ford Focus",
            DriveType = CarDriveType.FrontWheelDrive,
            SeatCount = 5,
            BodyType = BodyType.Hatchback,
            CarClass = CarClass.Economy
        },
        new()
        {
            Id = 4,
            Name = "Mercedes-Benz S-Class",
            DriveType = CarDriveType.RearWheelDrive,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            CarClass = CarClass.Business
        },
        new()
        {
            Id = 5,
            Name = "Porsche 911",
            DriveType = CarDriveType.RearWheelDrive,
            SeatCount = 2,
            BodyType = BodyType.Coupe,
            CarClass = CarClass.Sport
        }
    ];

    /// <summary>
    /// List of all model generations
    /// </summary>
    public List<ModelGeneration> ModelGenerations =>
    [
        new()
        {
            Id = 1,
            Year = 2020,
            EngineVolume = 2.5,
            TransmissionType = TransmissionType.Automatic,
            ModelId = 1,
            Model = CarModels[0],
            RentalCostPerHour = 15.0m
        },
        new()
        {
            Id = 2,
            Year = 2022,
            EngineVolume = 3.0,
            TransmissionType = TransmissionType.Automatic,
            ModelId = 2,
            Model = CarModels[1],
            RentalCostPerHour = 30.0m
        },
        new()
        {
            Id = 3,
            Year = 2021,
            EngineVolume = 1.5,
            TransmissionType = TransmissionType.Manual,
            ModelId = 3,
            Model = CarModels[2],
            RentalCostPerHour = 10.0m
        },
        new()
        {
            Id = 4,
            Year = 2023,
            EngineVolume = 3.5,
            TransmissionType = TransmissionType.Automatic,
            ModelId = 4,
            Model = CarModels[3],
            RentalCostPerHour = 40.0m
        },
        new()
        {
            Id = 5,
            Year = 2022,
            EngineVolume = 3.8,
            TransmissionType = TransmissionType.Robotic,
            ModelId = 5,
            Model = CarModels[4],
            RentalCostPerHour = 50.0m
        }
    ];

    /// <summary>
    /// List of all cars
    /// </summary>
    public List<Car> Cars =>
    [
        new()
        {
            Id = 1,
            ModelGenerationId = 1,
            ModelGeneration = ModelGenerations[0],
            LicensePlate = "A123BC",
            Color = "Black"
        },
        new()
        {
            Id = 2,
            ModelGenerationId = 2,
            ModelGeneration = ModelGenerations[1],
            LicensePlate = "B456DE",
            Color = "White"
        },
        new()
        {
            Id = 3,
            ModelGenerationId = 3,
            ModelGeneration = ModelGenerations[2],
            LicensePlate = "C789FG",
            Color = "Red"
        },
        new()
        {
            Id = 4,
            ModelGenerationId = 4,
            ModelGeneration = ModelGenerations[3],
            LicensePlate = "D012GH",
            Color = "Blue"
        },
        new()
        {
            Id = 5,
            ModelGenerationId = 5,
            ModelGeneration = ModelGenerations[4],
            LicensePlate = "E345IJ",
            Color = "Yellow"
        },
        new()
        {
            Id = 6,
            ModelGenerationId = 1,
            ModelGeneration = ModelGenerations[0],
            LicensePlate = "F678KL",
            Color = "Silver"
        },
        new()
        {
            Id = 7,
            ModelGenerationId = 2,
            ModelGeneration = ModelGenerations[1],
            LicensePlate = "G901MN",
            Color = "Black"
        },
        new() { Id = 8, ModelGenerationId = 3, ModelGeneration = ModelGenerations[2], LicensePlate = "H234NO", Color = "Green" },
        new() { Id = 9, ModelGenerationId = 4, ModelGeneration = ModelGenerations[3], LicensePlate = "I567PQ", Color = "Gray" },
        new() { Id = 10, ModelGenerationId = 5, ModelGeneration = ModelGenerations[4], LicensePlate = "J890RS", Color = "Blue" },
        new() { Id = 11, ModelGenerationId = 1, ModelGeneration = ModelGenerations[0], LicensePlate = "K123ST", Color = "White" },
        new() { Id = 12, ModelGenerationId = 2, ModelGeneration = ModelGenerations[1], LicensePlate = "L456UV", Color = "Red" }
    ];

    /// <summary>
    /// List of all clients
    /// </summary>
    public List<Client> Clients =>
    [
        new()
        {
            Id = 1,
            DriverLicenseNumber = "1234567890",
            FullName = "Ivanov Ivan Ivanovich",
            BirthDate = new DateOnly(1990, 5, 15)
        },
        new()
        {
            Id = 2,
            DriverLicenseNumber = "2345678901",
            FullName = "Petrov Petr Petrovich",
            BirthDate = new DateOnly(1985, 8, 22)
        },
        new()
        {
            Id = 3,
            DriverLicenseNumber = "3456789012",
            FullName = "Sidorova Maria Sergeevna",
            BirthDate = new DateOnly(1992, 3, 10)
        },
        new()
        {
            Id = 4,
            DriverLicenseNumber = "4567890123",
            FullName = "Kuznetsov Andrey Vladimirovich",
            BirthDate = new DateOnly(1988, 11, 30)
        },
        new()
        {
            Id = 5,
            DriverLicenseNumber = "5678901234",
            FullName = "Smirnova Olga Dmitrievna",
            BirthDate = new DateOnly(1995, 7, 5)
        },
        new() { Id = 6, DriverLicenseNumber = "6789012345", FullName = "Fedorov Alexey Nikolaevich", BirthDate = new DateOnly(1987, 9, 18) },
        new() { Id = 7, DriverLicenseNumber = "7890123456", FullName = "Nikolaeva Elena Viktorovna", BirthDate = new DateOnly(1993, 12, 3) },
        new() { Id = 8, DriverLicenseNumber = "8901234567", FullName = "Vasiliev Dmitry Olegovich", BirthDate = new DateOnly(1991, 4, 25) },
        new() { Id = 9, DriverLicenseNumber = "9012345678", FullName = "Pavlova Anna Ivanovna", BirthDate = new DateOnly(1994, 6, 30) },
        new() { Id = 10, DriverLicenseNumber = "0123456789", FullName = "Mikhailov Sergey Petrovich", BirthDate = new DateOnly(1989, 2, 14) }
    ];

    /// <summary>
    /// List of all rentals
    /// </summary>
    public List<Rental> Rentals =>
    [
        new()
        {
            Id = 1,
            CarId = 1,
            Car = Cars[0],
            ClientId = 1,
            Client = Clients[0],
            RentalStart = new DateTime(2024, 1, 15, 10, 30, 0),
            RentalHours = 24
        },
        new()
        {
            Id = 2,
            CarId = 2,
            Car = Cars[1],
            ClientId = 2,
            Client = Clients[1],
            RentalStart = new DateTime(2024, 1, 17, 14, 15, 0),
            RentalHours = 48
        },
        new()
        {
            Id = 3,
            CarId = 3,
            Car = Cars[2],
            ClientId = 3,
            Client = Clients[2],
            RentalStart = new DateTime(2024, 1, 19, 9, 0, 0),
            RentalHours = 12
        },
        new()
        {
            Id = 4,
            CarId = 4,
            Car = Cars[3],
            ClientId = 4,
            Client = Clients[3],
            RentalStart = new DateTime(2024, 1, 22, 11, 45, 0),
            RentalHours = 72
        },
        new()
        {
            Id = 5,
            CarId = 5,
            Car = Cars[4],
            ClientId = 5,
            Client = Clients[4],
            RentalStart = new DateTime(2024, 1, 13, 16, 20, 0),
            RentalHours = 24
        },
        new()
        {
            Id = 6,
            CarId = 1,
            Car = Cars[0],
            ClientId = 2,
            Client = Clients[1],
            RentalStart = new DateTime(2024, 1, 21, 13, 0, 0),
            RentalHours = 36
        },
        new()
        {
            Id = 7,
            CarId = 2,
            Car = Cars[1],
            ClientId = 1,
            Client = Clients[0],
            RentalStart = new DateTime(2024, 1, 10, 9, 15, 0),
            RentalHours = 24
        },
        new()
        {
            Id = 8,
            CarId = 3,
            Car = Cars[2],
            ClientId = 1,
            Client = Clients[0],
            RentalStart = new DateTime(2024, 1, 12, 11, 30, 0),
            RentalHours = 48
        },
        new()
        {
            Id = 9,
            CarId = 4,
            Car = Cars[3],
            ClientId = 3,
            Client = Clients[2],
            RentalStart = new DateTime(2024, 1, 5, 15, 45, 0),
            RentalHours = 24
        },
        new()
        {
            Id = 10,
            CarId = 5,
            Car = Cars[4],
            ClientId = 4,
            Client = Clients[3],
            RentalStart = new DateTime(2024, 1, 8, 10, 0, 0),
            RentalHours = 12
        }
    ];
}