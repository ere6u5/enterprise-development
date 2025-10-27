namespace CarRental.Domain.Models;

/// <summary>
/// Договор аренды автомобиля
/// </summary>
public class Rental
{
    /// <summary>
    /// Уникальный идентификатор аренды
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала аренды
    /// </summary>
    public required DateTime RentalDate { get; set; }

    /// <summary>
    /// Продолжительность аренды в часах
    /// </summary>
    public required int RentalHours { get; set; }

    /// <summary>
    /// Идентификатор арендованного автомобиля
    /// </summary>
    public required int CarId { get; set; }

    /// <summary>
    /// Арендованный автомобиль
    /// </summary>
    public required Car Car { get; set; } = null!;

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент, взявший автомобиль в аренду
    /// </summary>
    public required Client Client { get; set; } = null!;
}