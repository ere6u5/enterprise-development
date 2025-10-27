namespace CarRental.Models;

/// <summary>
/// Модель автомобиля с характеристиками
/// </summary>
public class CarModel
{
    /// <summary>
    /// Уникальный идентификатор модели
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название модели автомобиля
    /// </summary>
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тип привода (FWD, AWD, RWD)
    /// </summary>
    public required string DriveType { get; set; } = string.Empty;

    /// <summary>
    /// Количество посадочных мест
    /// </summary>
    public required int SeatsCount { get; set; }

    /// <summary>
    /// Тип кузова (Sedan, SUV, Hatchback)
    /// </summary>
    public required string BodyType { get; set; } = string.Empty;

    /// <summary>
    /// Класс автомобиля (Economy, Business, Premium)
    /// </summary>
    public required string Class { get; set; } = string.Empty;
}