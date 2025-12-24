using Application.Dto;
using Domain.Entities;

namespace Application.Service;

/// <summary>
/// Сервис для работы с автомобилями
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Создание нового автомобиля
    /// </summary>
    /// <param name="entity">Данные автомобиля</param>
    /// <returns>Идентификатор созданного автомобиля</returns>
    public Task<int> CreateCarAsync(CarDto entity);
    
    /// <summary>
    /// Получение всех автомобилей
    /// </summary>
    /// <returns>Список автомобилей</returns>
    public Task<List<CarResponseDto>> GetAllCarsAsync();
    
    /// <summary>
    /// Получение автомобиля по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <returns>Автомобиль</returns>
    public Task<CarResponseDto?> GetCarAsync(int id);
    
    /// <summary>
    /// Обновление автомобиля
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <param name="entity">Новые данные автомобиля</param>
    /// <returns>Обновленный автомобиль</returns>
    public Task<CarDto?> UpdateCarAsync(int id, CarDto entity);
    
    /// <summary>
    /// Удаление автомобиля
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <returns>True, если удаление прошло успешно</returns>
    public Task<bool> DeleteCarAsync(int id);
    
    /// <summary>
    /// Получение всех автомобилей с идентификаторами
    /// </summary>
    /// <returns>Список автомобилей</returns>
    public Task<List<Car>> GetAllCarsWithIdAsync();
}