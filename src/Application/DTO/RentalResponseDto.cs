namespace Application.DTO;

/// <summary>
/// Response DTO для аренды
/// </summary>
public class RentalResponseDto
{
    /// <summary>
    /// Идентификатор аренды
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int CarId { get; set; }
    
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }
    
    /// <summary>
    /// Время выдачи автомобиля
    /// </summary>
    public required DateTime RentalStart { get; set; }
    
    /// <summary>
    /// Время аренды в часах
    /// </summary>
    public required int RentalHours { get; set; }
}