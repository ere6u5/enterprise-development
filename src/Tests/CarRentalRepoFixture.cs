using Application.Service;
using Infrastructure.InMemory.Repositories;
using Domain.Seeder;

namespace Tests;

/// <summary>
/// Test fixture for car rental repository tests
/// </summary>
public class CarRentalRepoFixture
{
    /// <summary>
    /// Car model service instance
    /// </summary>
    public CarModelService CarModelService { get; }

    /// <summary>
    /// Model generation service instance
    /// </summary>
    public ModelGenerationService ModelGenerationService { get; }

    /// <summary>
    /// Car service instance
    /// </summary>
    public CarService CarService { get; }

    /// <summary>
    /// Client service instance
    /// </summary>
    public ClientService ClientService { get; }

    /// <summary>
    /// Rental service instance
    /// </summary>
    public RentalService RentalService { get; }

    /// <summary>
    /// Initializes a new instance of the car rental repository fixture
    /// </summary>
    public CarRentalRepoFixture()
    {
        var seeder = new DataSeeder();

        var carModelRepository = new InMemoryCarModelRepository(seeder);
        var modelGenerationRepository = new InMemoryModelGenerationRepository(seeder);
        var carRepository = new InMemoryCarRepository(seeder);
        var clientRepository = new InMemoryClientRepository(seeder);
        var rentalRepository = new InMemoryRentalRepository(seeder);

        CarModelService = new CarModelService(carModelRepository);
        ModelGenerationService = new ModelGenerationService(modelGenerationRepository, carModelRepository);
        CarService = new CarService(carRepository, modelGenerationRepository);
        ClientService = new ClientService(clientRepository);
        RentalService = new RentalService(rentalRepository, carRepository, clientRepository, carModelRepository, modelGenerationRepository);
    }
}