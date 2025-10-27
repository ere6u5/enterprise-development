namespace CarRental.Data.Dtos;

/// <summary>
/// Data Transfer Object для представления аренды
/// </summary>
public class RentalDto
{
    /// <summary>
    /// Уникальный идентификатор аренды
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала аренды
    /// </summary>
    public DateTime RentalDate { get; set; }

    /// <summary>
    /// Продолжительность аренды в часах
    /// </summary>
    public int RentalHours { get; set; }

    /// <summary>
    /// Идентификатор арендованного автомобиля
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Государственный номерной знак арендованного автомобиля
    /// </summary>
    public string CarLicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Модель арендованного автомобиля
    /// </summary>
    public string CarModel { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Имя клиента
    /// </summary>
    public string ClientName { get; set; } = string.Empty;

    /// <summary>
    /// Общая стоимость аренды
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Флаг, указывающий активна ли аренда в данный момент
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// Data Transfer Object для создания новой аренды
/// </summary>
public class CreateRentalDto
{
    /// <summary>
    /// Идентификатор арендуемого автомобиля
    /// </summary>
    public required int CarId { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Продолжительность аренды в часах
    /// </summary>
    public required int RentalHours { get; set; }
}