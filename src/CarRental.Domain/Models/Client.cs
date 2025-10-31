namespace CarRental.Domain.Models;

/// <summary>
/// Клиент службы проката автомобилей
/// </summary>
public class Client
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public required DateOnly BirthDate { get; set; }
}