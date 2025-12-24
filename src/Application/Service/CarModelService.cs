using Application.Dto;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Service;

/// <summary>
/// Сервис для работы с моделями автомобилей
/// </summary>
/// <param name="carModelRepository">Репозиторий моделей автомобилей</param>
public class CarModelService(IRepository<CarModel> carModelRepository) : ICarModelService
{
    /// <summary>
    /// Маппинг Dto в доменную модель
    /// </summary>
    public static CarModel MapToDomain(CarModelDto entity)
    {
        return new CarModel
        {
            Id = 0,
            Name = entity.Name,
            DriveType = entity.DriveType,
            SeatCount = entity.SeatCount,
            BodyType = entity.BodyType,
            CarClass = entity.CarClass
        };
    }
    
    /// <summary>
    /// Маппинг доменной модели в Dto
    /// </summary>
    public static CarModelDto MapToDto(CarModel carModel)
    {
        return new CarModelDto
        {
            Name = carModel.Name,
            DriveType = carModel.DriveType,
            SeatCount = carModel.SeatCount,
            BodyType = carModel.BodyType,
            CarClass = carModel.CarClass
        };
    }
    
    /// <summary>
    /// Маппинг доменной модели в Response Dto
    /// </summary>
    public static CarModelResponseDto MapToResponseDto(CarModel carModel)
    {
        return new CarModelResponseDto
        {
            Id = carModel.Id,
            Name = carModel.Name,
            DriveType = carModel.DriveType,
            SeatCount = carModel.SeatCount,
            BodyType = carModel.BodyType,
            CarClass = carModel.CarClass
        };
    }
    
    /// <inheritdoc />
    public async Task<int> CreateCarModelAsync(CarModelDto entity)
    {
        var carModel = MapToDomain(entity);
        return await carModelRepository.CreateAsync(carModel);
    }
    
    /// <inheritdoc />
    public async Task<List<CarModelResponseDto>> GetAllCarModelsAsync()
    {
        var carModels = await carModelRepository.ReadAsync();
        return [.. carModels.Select(MapToResponseDto)];
    }
    
    /// <inheritdoc />
    public async Task<CarModelResponseDto?> GetCarModelAsync(int id)
    {
        var carModel = await carModelRepository.ReadAsync(id);
        return carModel != null ? MapToResponseDto(carModel) : null;
    }
    
    /// <inheritdoc />
    public async Task<CarModelDto?> UpdateCarModelAsync(int id, CarModelDto entity)
    {
        var carModelToUpdate = MapToDomain(entity);
        var updatedCarModel = await carModelRepository.UpdateAsync(id, carModelToUpdate);
        return updatedCarModel != null ? MapToDto(updatedCarModel) : null;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteCarModelAsync(int id)
    {
        return await carModelRepository.DeleteAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<List<CarModel>> GetAllCarModelsWithIdAsync()
    {
        return await carModelRepository.ReadAsync();
    }
}