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
    public required string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Цвет автомобиля
    /// </summary>
    public required string Color { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор поколения модели
    /// </summary>
    public int ModelGenerationId { get; set; }

    /// <summary>
    /// Поколение модели автомобиля
    /// </summary>
    public required ModelGeneration ModelGeneration { get; set; } = null!;

    public Car() { }

    public Car(string licensePlate, string color, ModelGeneration modelGeneration)
    {
        LicensePlate = licensePlate;
        Color = color;
        ModelGeneration = modelGeneration;
        ModelGenerationId = modelGeneration.Id;
    }
}