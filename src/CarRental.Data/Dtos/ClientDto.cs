namespace CarRental.Data.Dtos;

/// <summary>
/// Data Transfer Object для представления клиента
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер водительского удостоверения клиента
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

    /// <summary>
    /// Возраст клиента (вычисляемое свойство)
    /// </summary>
    public int Age => DateTime.Now.Year - BirthDate.Year;
}

/// <summary>
/// Data Transfer Object для создания нового клиента
/// </summary>
public class CreateClientDto
{
    /// <summary>
    /// Номер водительского удостоверения клиента
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public required DateTime BirthDate { get; set; }
}

/// <summary>
/// Data Transfer Object для клиента с общей суммой аренд
/// </summary>
public class ClientWithRentalSumDto
{
    /// <summary>
    /// Данные клиента
    /// </summary>
    public ClientDto Client { get; set; } = null!;

    /// <summary>
    /// Общая сумма всех аренд клиента
    /// </summary>
    public decimal TotalRentalSum { get; set; }
}