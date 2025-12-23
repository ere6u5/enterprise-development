using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Модель автомобиля (справочник)
/// </summary>
public class CarModel
{
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Название модели
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Тип привода
    /// </summary>
    public required CarDriveType DriveType { get; set; }
    
    /// <summary>
    /// Число посадочных мест
    /// </summary>
    public required int SeatCount { get; set; }
    
    /// <summary>
    /// Тип кузова
    /// </summary>
    public required BodyType BodyType { get; set; }
    
    /// <summary>
    /// Класс автомобиля
    /// </summary>
    public required CarClass CarClass { get; set; }
}