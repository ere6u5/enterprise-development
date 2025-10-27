namespace CarRental.Models;

/// <summary>
/// Поколение модели автомобиля с техническими характеристиками
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Уникальный идентификатор поколения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Год выпуска поколения
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Объем двигателя в литрах
    /// </summary>
    public double EngineVolume { get; set; }

    /// <summary>
    /// Тип коробки передач
    /// </summary>
    public string Transmission { get; set; } = string.Empty;

    /// <summary>
    /// Стоимость аренды в час
    /// </summary>
    public decimal RentalPricePerHour { get; set; }

    /// <summary>
    /// Идентификатор модели автомобиля
    /// </summary>
    public int ModelId { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public CarModel Model { get; set; } = null!;
}