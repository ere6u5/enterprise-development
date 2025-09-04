using CarRental.Domain.Enums;
using CarRental.Domain.Model;
using Bogus;

namespace CarRental.Infrastructure.DataSeed;

public static class CarRentalSeeder
{
    public static List<CarModel> GetCarModels()
    {
        return new List<CarModel>
        {
            new CarModel { Id = 1, Name = "Toyota Camry", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.Sedan, Class = CarClass.Comfort },
            new CarModel { Id = 2, Name = "BMW X5", DriveType = WheelDriveType.AllWheel, SeatsCount = 5, BodyType = BodyType.SUV, Class = CarClass.Premium },
            new CarModel { Id = 3, Name = "Honda Civic", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.Hatchback, Class = CarClass.Economy },
            new CarModel { Id = 4, Name = "Mercedes E-Class", DriveType = WheelDriveType.RearWheel, SeatsCount = 5, BodyType = BodyType.Sedan, Class = CarClass.Business },
            new CarModel { Id = 5, Name = "Ford Focus", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.Hatchback, Class = CarClass.Economy },
            new CarModel { Id = 6, Name = "Audi A6", DriveType = WheelDriveType.AllWheel, SeatsCount = 5, BodyType = BodyType.Sedan, Class = CarClass.Business },
            new CarModel { Id = 7, Name = "Volkswagen Golf", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.Hatchback, Class = CarClass.Economy },
            new CarModel { Id = 8, Name = "Lexus RX", DriveType = WheelDriveType.AllWheel, SeatsCount = 5, BodyType = BodyType.SUV, Class = CarClass.Premium },
            new CarModel { Id = 9, Name = "Hyundai Tucson", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.SUV, Class = CarClass.Comfort },
            new CarModel { Id = 10, Name = "Kia Rio", DriveType = WheelDriveType.FrontWheel, SeatsCount = 5, BodyType = BodyType.Sedan, Class = CarClass.Economy }
        };
    }

    public static List<ModelGeneration> GetModelGenerations()
    {
        var models = GetCarModels();
        var faker = new Faker();

        var generations = new List<ModelGeneration>();
        int generationId = 1;

        foreach (var model in models)
        {
            for (int i = 0; i < 3; i++) // 3 поколения для каждой модели
            {
                generations.Add(new ModelGeneration
                {
                    Id = generationId++,
                    ModelId = model.Id,
                    Model = model,
                    ReleaseYear = faker.Random.Int(2015, 2023),
                    EngineVolume = faker.Random.Decimal(1.6m, 3.0m),
                    Transmission = faker.PickRandom<TransmissionType>(),
                    RentalCostPerHour = faker.Random.Decimal(500, 3000)
                });
            }
        }

        return generations;
    }

    public static List<Car> GetCars()
    {
        var generations = GetModelGenerations();
        var faker = new Faker();
        var colors = new[] { "Black", "White", "Silver", "Gray", "Red", "Blue", "Green" };

        var cars = new List<Car>();

        for (int i = 1; i <= 30; i++)
        {
            var generation = faker.PickRandom(generations);
            cars.Add(new Car
            {
                Id = i,
                GenerationId = generation.Id,
                Generation = generation,
                LicensePlate = GenerateLicensePlate(faker),
                Color = faker.PickRandom(colors)
            });
        }

        return cars;
    }

    public static List<Customer> GetCustomers()
    {
        var faker = new Faker("ru");

        return Enumerable.Range(1, 15).Select(i => new Customer
        {
            Id = i,
            DriverLicenseNumber = GenerateDriverLicense(faker),
            FullName = faker.Name.FullName(),
            BirthDate = DateOnly.FromDateTime(faker.Date.Between(new DateTime(1970, 1, 1), new DateTime(2003, 12, 31)))
        }).ToList();
    }

    public static List<Rental> GetRentals()
    {
        var cars = GetCars();
        var customers = GetCustomers();
        var faker = new Faker();

        var rentals = new List<Rental>();
        var rentalId = 1;

        // Создаем аренды для каждого клиента
        foreach (var customer in customers)
        {
            var customerRentalsCount = faker.Random.Int(1, 8);

            for (int i = 0; i < customerRentalsCount; i++)
            {
                var car = faker.PickRandom(cars);
                var startDate = faker.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now);

                rentals.Add(new Rental
                {
                    Id = rentalId++,
                    CarId = car.Id,
                    Car = car,
                    CustomerId = customer.Id,
                    Customer = customer,
                    RentalStart = startDate,
                    RentalHours = faker.Random.Int(1, 72)
                });
            }
        }

        return rentals;
    }

    private static string GenerateLicensePlate(Faker faker)
    {
        var letters = "АВЕКМНОРСТУХ".ToCharArray();
        return $"{faker.PickRandom(letters)}{faker.PickRandom(letters)}{faker.Random.Int(100, 999)}{faker.PickRandom(letters)}{faker.PickRandom(letters)}";
    }

    private static string GenerateDriverLicense(Faker faker)
    {
        return $"{faker.Random.Int(10, 99)} {faker.Random.Int(10, 99)} {faker.Random.Int(100000, 999999)}";
    }
}