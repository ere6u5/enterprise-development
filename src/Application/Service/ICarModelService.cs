using Application.Dto;
using Domain.Entities;

namespace Application.Service;

/// <summary>
/// Сервис для работы с моделями автомобилей
/// </summary>
public interface ICarModelService
{
    /// <summary>
    /// Создание новой модели автомобиля
    /// </summary>
    /// <param name="entity">Данные модели</param>
    /// <returns>Идентификатор созданной модели</returns>
    public Task<int> CreateCarModelAsync(CarModelDto entity);
    
    /// <summary>
    /// Получение всех моделей автомобилей
    /// </summary>
    /// <returns>Список моделей</returns>
    public Task<List<CarModelResponseDto>> GetAllCarModelsAsync();
    
    /// <summary>
    /// Получение модели автомобиля по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор модели</param>
    /// <returns>Модель автомобиля</returns>
    public Task<CarModelResponseDto?> GetCarModelAsync(int id);
    
    /// <summary>
    /// Обновление модели автомобиля
    /// </summary>
    /// <param name="id">Идентификатор модели</param>
    /// <param name="entity">Новые данные модели</param>
    /// <returns>Обновленная модель</returns>
    public Task<CarModelDto?> UpdateCarModelAsync(int id, CarModelDto entity);
    
    /// <summary>
    /// Удаление модели автомобиля
    /// </summary>
    /// <param name="id">Идентификатор модели</param>
    /// <returns>True, если удаление прошло успешно</returns>
    public Task<bool> DeleteCarModelAsync(int id);
    
    /// <summary>
    /// Получение всех моделей автомобилей с идентификаторами
    /// </summary>
    /// <returns>Список моделей</returns>
    public Task<List<CarModel>> GetAllCarModelsWithIdAsync();
}