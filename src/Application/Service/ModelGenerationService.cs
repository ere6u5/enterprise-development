using Application.Dto;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Service;

/// <summary>
/// Сервис для работы с поколениями моделей
/// </summary>
/// <param name="modelGenerationRepository">Репозиторий поколений моделей</param>
/// <param name="carModelRepository">Репозиторий моделей автомобилей</param>
public class ModelGenerationService(IRepository<ModelGeneration> modelGenerationRepository, IRepository<CarModel> carModelRepository) : IModelGenerationService
{
    /// <summary>
    /// Маппинг Dto в доменную модель
    /// </summary>
    private async Task<ModelGeneration> MapToDomainAsync(ModelGenerationDto entity)
    {
        var carModel = await carModelRepository.ReadAsync(entity.ModelId)
            ?? throw new ArgumentException($"Car model with id {entity.ModelId} not found");

        return new ModelGeneration
        {
            Id = 0,
            Year = entity.Year,
            EngineVolume = entity.EngineVolume,
            TransmissionType = entity.TransmissionType,
            ModelId = entity.ModelId,
            Model = carModel,
            RentalCostPerHour = entity.RentalCostPerHour
        };
    }

    /// <summary>
    /// Маппинг доменной модели в Dto
    /// </summary>
    public static ModelGenerationDto MapToDto(ModelGeneration modelGeneration)
    {
        return new ModelGenerationDto
        {
            Year = modelGeneration.Year,
            EngineVolume = modelGeneration.EngineVolume,
            TransmissionType = modelGeneration.TransmissionType,
            ModelId = modelGeneration.ModelId,
            RentalCostPerHour = modelGeneration.RentalCostPerHour
        };
    }

    /// <summary>
    /// Маппинг доменной модели в Response Dto
    /// </summary>
    public static ModelGenerationResponseDto MapToResponseDto(ModelGeneration modelGeneration)
    {
        return new ModelGenerationResponseDto
        {
            Id = modelGeneration.Id,
            Year = modelGeneration.Year,
            EngineVolume = modelGeneration.EngineVolume,
            TransmissionType = modelGeneration.TransmissionType,
            Model = CarModelService.MapToResponseDto(modelGeneration.Model),
            RentalCostPerHour = modelGeneration.RentalCostPerHour
        };
    }

    /// <inheritdoc />
    public async Task<int> CreateModelGenerationAsync(ModelGenerationDto entity)
    {
        var modelGeneration = await MapToDomainAsync(entity);
        return await modelGenerationRepository.CreateAsync(modelGeneration);
    }

    /// <inheritdoc />
    public async Task<List<ModelGenerationResponseDto>> GetAllModelGenerationsAsync()
    {
        var modelGenerations = await modelGenerationRepository.ReadAsync();
        return [.. modelGenerations.Select(MapToResponseDto)];
    }

    /// <inheritdoc />
    public async Task<ModelGenerationResponseDto?> GetModelGenerationAsync(int id)
    {
        var modelGeneration = await modelGenerationRepository.ReadAsync(id);
        return modelGeneration != null ? MapToResponseDto(modelGeneration) : null;
    }

    /// <inheritdoc />
    public async Task<ModelGenerationDto?> UpdateModelGenerationAsync(int id, ModelGenerationDto entity)
    {
        var modelGenerationToUpdate = await MapToDomainAsync(entity);
        var updatedModelGeneration = await modelGenerationRepository.UpdateAsync(id, modelGenerationToUpdate);
        return updatedModelGeneration != null ? MapToDto(updatedModelGeneration) : null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteModelGenerationAsync(int id)
    {
        return await modelGenerationRepository.DeleteAsync(id);
    }
}