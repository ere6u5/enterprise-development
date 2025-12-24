namespace Application.Dto;

/// <summary>
/// Dto для клиента с суммой аренды
/// </summary>
public class ClientRentalSumDto
{
    /// <summary>
    /// Клиент
    /// </summary>
    public required ClientResponseDto Client { get; set; }
    
    /// <summary>
    /// Сумма аренды
    /// </summary>
    public required decimal TotalRentalCost { get; set; }
}