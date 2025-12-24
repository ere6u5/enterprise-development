namespace Application.Dto;

/// <summary>
/// Dto для клиента
/// </summary>
public class ClientDto
{
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