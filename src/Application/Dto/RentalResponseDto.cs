namespace Application.Dto;

/// <summary>
/// Response Dto для аренды
/// </summary>
public class RentalResponseDto
{
    /// <summary>
    /// Идентификатор аренды
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Автомобиль
    /// </summary>
    public required CarResponseDto Car { get; set; }
    
    /// <summary>
    /// Клиент
    /// </summary>
    public required ClientResponseDto Client { get; set; }
    
    /// <summary>
    /// Время выдачи автомобиля
    /// </summary>
    public required DateTime RentalStart { get; set; }
    
    /// <summary>
    /// Время аренды в часах
    /// </summary>
    public required int RentalHours { get; set; }
}