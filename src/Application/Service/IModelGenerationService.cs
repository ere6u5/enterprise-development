using Application.Dto;
using Domain.Entities;

namespace Application.Service;

/// <summary>
/// Сервис для работы с поколениями моделей
/// </summary>
public interface IModelGenerationService
{
    /// <summary>
    /// Создание нового поколения модели
    /// </summary>
    /// <param name="entity">Данные поколения</param>
    /// <returns>Идентификатор созданного поколения</returns>
    public Task<int> CreateModelGenerationAsync(ModelGenerationDto entity);
    
    /// <summary>
    /// Получение всех поколений моделей
    /// </summary>
    /// <returns>Список поколений</returns>
    public Task<List<ModelGenerationResponseDto>> GetAllModelGenerationsAsync();
    
    /// <summary>
    /// Получение поколения модели по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор поколения</param>
    /// <returns>Поколение модели</returns>
    public Task<ModelGenerationResponseDto?> GetModelGenerationAsync(int id);
    
    /// <summary>
    /// Обновление поколения модели
    /// </summary>
    /// <param name="id">Идентификатор поколения</param>
    /// <param name="entity">Новые данные поколения</param>
    /// <returns>Обновленное поколение</returns>
    public Task<ModelGenerationDto?> UpdateModelGenerationAsync(int id, ModelGenerationDto entity);
    
    /// <summary>
    /// Удаление поколения модели
    /// </summary>
    /// <param name="id">Идентификатор поколения</param>
    /// <returns>True, если удаление прошло успешно</returns>
    public Task<bool> DeleteModelGenerationAsync(int id);
}