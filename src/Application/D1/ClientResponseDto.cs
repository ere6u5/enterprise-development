namespace Application.Dto;

/// <summary>
/// Response Dto для клиента
/// </summary>
public class ClientResponseDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    public required string DriverLicenseNumber { get; set; }
    
    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}