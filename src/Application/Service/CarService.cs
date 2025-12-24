using Application.Dto;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Service;

/// <summary>
/// Сервис для работы с автомобилями
/// </summary>
/// <param name="carRepository">Репозиторий автомобилей</param>
/// <param name="modelGenerationRepository">Репозиторий поколений моделей</param>
public class CarService(IRepository<Car> carRepository, IRepository<ModelGeneration> modelGenerationRepository) : ICarService
{
    /// <summary>
    /// Маппинг Dto в доменную модель
    /// </summary>
    private async Task<Car> MapToDomainAsync(CarDto entity)
    {
        var modelGeneration = await modelGenerationRepository.ReadAsync(entity.ModelGenerationId)
            ?? throw new ArgumentException($"Model generation with id {entity.ModelGenerationId} not found");

        return new Car
        {
            Id = 0,
            ModelGenerationId = entity.ModelGenerationId,
            ModelGeneration = modelGeneration,
            LicensePlate = entity.LicensePlate,
            Color = entity.Color
        };
    }

    /// <summary>
    /// Маппинг доменной модели в Dto
    /// </summary>
    private static CarDto MapToDto(Car car)
    {
        return new CarDto
        {
            ModelGenerationId = car.ModelGenerationId,
            LicensePlate = car.LicensePlate,
            Color = car.Color
        };
    }

    /// <summary>
    /// Маппинг доменной модели в Response Dto
    /// </summary>
    private static CarResponseDto MapToResponseDto(Car car)
    {
        return new CarResponseDto
        {
            Id = car.Id,
            ModelGeneration = new ModelGenerationResponseDto
            {
                Id = car.ModelGeneration.Id,
                Year = car.ModelGeneration.Year,
                EngineVolume = car.ModelGeneration.EngineVolume,
                TransmissionType = car.ModelGeneration.TransmissionType,
                Model = new CarModelResponseDto
                {
                    Id = car.ModelGeneration.Model.Id,
                    Name = car.ModelGeneration.Model.Name,
                    DriveType = car.ModelGeneration.Model.DriveType,
                    SeatCount = car.ModelGeneration.Model.SeatCount,
                    BodyType = car.ModelGeneration.Model.BodyType,
                    CarClass = car.ModelGeneration.Model.CarClass
                },
                RentalCostPerHour = car.ModelGeneration.RentalCostPerHour
            },
            LicensePlate = car.LicensePlate,
            Color = car.Color
        };
    }

    /// <inheritdoc />
    public async Task<int> CreateCarAsync(CarDto entity)
    {
        var car = await MapToDomainAsync(entity);
        return await carRepository.CreateAsync(car);
    }

    /// <inheritdoc />
    public async Task<List<CarResponseDto>> GetAllCarsAsync()
    {
        var cars = await carRepository.ReadAsync();
        return [.. cars.Select(MapToResponseDto)];
    }

    /// <inheritdoc />
    public async Task<CarResponseDto?> GetCarAsync(int id)
    {
        var car = await carRepository.ReadAsync(id);
        return car != null ? MapToResponseDto(car) : null;
    }

    /// <inheritdoc />
    public async Task<CarDto?> UpdateCarAsync(int id, CarDto entity)
    {
        var carToUpdate = await MapToDomainAsync(entity);
        var updatedCar = await carRepository.UpdateAsync(id, carToUpdate);
        return updatedCar != null ? MapToDto(updatedCar) : null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCarAsync(int id)
    {
        return await carRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<List<Car>> GetAllCarsWithIdAsync()
    {
        return await carRepository.ReadAsync();
    }
}