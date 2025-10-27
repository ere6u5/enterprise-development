namespace CarRental.Models;

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
    /// Арендованный автомобиль
    /// </summary>
    public Car Car { get; set; } = null!;

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент, взявший автомобиль в аренду
    /// </summary>
    public Client Client { get; set; } = null!;
}