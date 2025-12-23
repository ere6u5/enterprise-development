using Application.DTO;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Service;

/// <summary>
/// Сервис для работы с арендой
/// </summary>
/// <param name="rentalRepository">Репозиторий аренд</param>
/// <param name="carRepository">Репозиторий автомобилей</param>
/// <param name="clientRepository">Репозиторий клиентов</param>
/// <param name="carModelRepository">Репозиторий моделей автомобилей</param>
/// <param name="modelGenerationRepository">Репозиторий поколений моделей</param>
public class RentalService(
    IRepository<Rental> rentalRepository,
    IRepository<Car> carRepository,
    IRepository<Client> clientRepository,
    IRepository<CarModel> carModelRepository,
    IRepository<ModelGeneration> modelGenerationRepository) : IRentalService
{
    /// <summary>
    /// Маппинг DTO в доменную модель
    /// </summary>
    private async Task<Rental> MapToDomainAsync(RentalDto entity)
    {
        var car = await carRepository.ReadAsync(entity.CarId) 
            ?? throw new ArgumentException($"Car with id {entity.CarId} not found");
        var client = await clientRepository.ReadAsync(entity.ClientId) 
            ?? throw new ArgumentException($"Client with id {entity.ClientId} not found");
        
        return new Rental
        {
            Id = 0,
            CarId = entity.CarId,
            Car = car,
            ClientId = entity.ClientId,
            Client = client,
            RentalStart = entity.RentalStart,
            RentalHours = entity.RentalHours
        };
    }
    
    /// <summary>
    /// Маппинг доменной модели в Response DTO
    /// </summary>
    private static RentalResponseDto MapToResponseDto(Rental rental)
    {
        return new RentalResponseDto
        {
            Id = rental.Id,
            CarId = rental.CarId,
            ClientId = rental.ClientId,
            RentalStart = rental.RentalStart,
            RentalHours = rental.RentalHours
        };
    }
    
    /// <summary>
    /// Маппинг автомобиля в CarResponseDto
    /// </summary>
    private static CarResponseDto MapToCarResponseDto(Car car)
    {
        return new CarResponseDto
        {
            Id = car.Id,
            ModelGenerationId = car.ModelGenerationId,
            LicensePlate = car.LicensePlate,
            Color = car.Color
        };
    }
    
    /// <summary>
    /// Маппинг клиента в ClientResponseDto
    /// </summary>
    private static ClientResponseDto MapToClientResponseDto(Client client)
    {
        return new ClientResponseDto
        {
            Id = client.Id,
            DriverLicenseNumber = client.DriverLicenseNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate
        };
    }
    
    /// <inheritdoc />
    public async Task<int> CreateRentalAsync(RentalDto entity)
    {
        var rental = await MapToDomainAsync(entity);
        return await rentalRepository.CreateAsync(rental);
    }
    
    /// <inheritdoc />
    public async Task<List<RentalResponseDto>> GetAllRentalsAsync()
    {
        var rentals = await rentalRepository.ReadAsync();
        return [.. rentals.Select(MapToResponseDto)];
    }
    
    /// <inheritdoc />
    public async Task<RentalResponseDto?> GetRentalAsync(int id)
    {
        var rental = await rentalRepository.ReadAsync(id);
        return rental != null ? MapToResponseDto(rental) : null;
    }
    
    /// <inheritdoc />
    public async Task<RentalResponseDto?> UpdateRentalAsync(int id, RentalDto entity)
    {
        var rentalToUpdate = await MapToDomainAsync(entity);
        var updatedRental = await rentalRepository.UpdateAsync(id, rentalToUpdate);
        return updatedRental != null ? MapToResponseDto(updatedRental) : null;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteRentalAsync(int id)
    {
        return await rentalRepository.DeleteAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<List<ClientResponseDto>> GetClientsByModelAsync(int modelId)
    {
        var rentals = await rentalRepository.ReadAsync();
        var carModels = await carModelRepository.ReadAsync();
        
        return [.. rentals
            .Where(r => r.Car.ModelGeneration.Model.Id == modelId)
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .Select(MapToClientResponseDto)];
    }
    
    /// <inheritdoc />
    public async Task<List<CarResponseDto>> GetRentedCarsAsync()
    {
        var rentals = await rentalRepository.ReadAsync();
        var currentTime = DateTime.Now;
        
        return [.. rentals
            .Where(r => r.RentalStart <= currentTime && r.RentalStart.AddHours(r.RentalHours) >= currentTime)
            .Select(r => r.Car)
            .Distinct()
            .Select(MapToCarResponseDto)];
    }
    
    /// <inheritdoc />
    public async Task<List<CarRentalCountDto>> GetTop5MostRentedCarsAsync()
    {
        var rentals = await rentalRepository.ReadAsync();
        var cars = await carRepository.ReadAsync();
        
        var result = rentals
            .GroupBy(r => r.CarId)
            .Select(g => new CarRentalCountDto
            {
                Car = MapToCarResponseDto(cars.First(c => c.Id == g.Key)),
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();
        
        return result;
    }
    
    /// <inheritdoc />
    public async Task<List<CarRentalCountDto>> GetRentalCountPerCarAsync()
    {
        var rentals = await rentalRepository.ReadAsync();
        var cars = await carRepository.ReadAsync();
        
        var result = cars
            .Select(car => new CarRentalCountDto
            {
                Car = MapToCarResponseDto(car),
                RentalCount = rentals.Count(r => r.CarId == car.Id)
            })
            .ToList();
        
        return result;
    }
    
    /// <inheritdoc />
    public async Task<List<ClientRentalSumDto>> GetTop5ClientsByRentalSumAsync()
    {
        var rentals = await rentalRepository.ReadAsync();
        var clients = await clientRepository.ReadAsync();
        var modelGenerations = await modelGenerationRepository.ReadAsync();
        
        var result = clients
            .Select(client => new ClientRentalSumDto
            {
                Client = MapToClientResponseDto(client),
                TotalRentalCost = rentals
                    .Where(r => r.ClientId == client.Id)
                    .Sum(r => 
                    {
                        var car = r.Car;
                        var modelGeneration = modelGenerations.FirstOrDefault(mg => mg.Id == car.ModelGenerationId);
                        return modelGeneration?.RentalCostPerHour * r.RentalHours ?? 0;
                    })
            })
            .OrderByDescending(x => x.TotalRentalCost)
            .Take(5)
            .ToList();
        
        return result;
    }
}