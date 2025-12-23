namespace Application.DTO;

/// <summary>
/// DTO для автомобиля с количеством аренд
/// </summary>
public class CarRentalCountDto
{
    /// <summary>
    /// Автомобиль
    /// </summary>
    public required CarResponseDto Car { get; set; }
    
    /// <summary>
    /// Количество аренд
    /// </summary>
    public required int RentalCount { get; set; }
}