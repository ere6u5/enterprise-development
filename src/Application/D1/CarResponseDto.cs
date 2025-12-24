namespace Application.Dto;

/// <summary>
/// Response Dto для автомобиля
/// </summary>
public class CarResponseDto
{
    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Поколение модели
    /// </summary>
    public required ModelGenerationResponseDto ModelGeneration { get; set; }
    
    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }
    
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Color { get; set; }
}