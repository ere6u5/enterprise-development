namespace CarRental.Models;

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
    public required string LicenseNumber { get; set; } = string.Empty;

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    public required string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public required DateTime BirthDate { get; set; }
}