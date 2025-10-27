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
    public string LicenseNumber { get; set; } = string.Empty;

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public DateTime BirthDate { get; set; }
}