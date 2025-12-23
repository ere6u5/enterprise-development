namespace Application.DTO;

/// <summary>
/// Response DTO для автомобиля
/// </summary>
public class CarResponseDto
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
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }
    
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Color { get; set; }
}