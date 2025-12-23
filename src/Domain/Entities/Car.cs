namespace Domain.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public class Car
{
    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Идентификатор поколения модели
    /// </summary>
    public required int ModelGenerationId { get; set; }
    
    /// <summary>
    /// Поколение модели
    /// </summary>
    public required ModelGeneration ModelGeneration { get; set; }
    
    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }
    
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Color { get; set; }
}