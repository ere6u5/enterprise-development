namespace CarRental.Domain.Models;

/// <summary>
/// Конкретный автомобиль в парке проката
/// </summary>
public class Car
{
    /// <summary>
    /// Уникальный идентификатор автомобиля
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Государственный номерной знак
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Цвет автомобиля
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Идентификатор поколения модели
    /// </summary>
    public required int ModelGenerationId { get; set; }

    /// <summary>
    /// Поколение модели автомобиля
    /// </summary>
    public required ModelGeneration ModelGeneration { get; set; }
}