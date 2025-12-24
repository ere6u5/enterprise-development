namespace Application.Dto;

/// <summary>
/// Dto для автомобиля
/// </summary>
public class CarDto
{
    /// <summary>
    /// Идентификатор поколения модели
    /// </summary>
    public required int ModelGenerationId { get; set; }
    
    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }
    
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Color { get; set; }
}