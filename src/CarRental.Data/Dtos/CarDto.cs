namespace CarRental.Data.Dtos;

/// <summary>
/// Data Transfer Object для представления автомобиля
/// </summary>
public class CarDto
{
    /// <summary>
    /// Уникальный идентификатор автомобиля
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Государственный номерной знак автомобиля
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Цвет автомобиля
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор поколения модели автомобиля
    /// </summary>
    public int ModelGenerationId { get; set; }

    /// <summary>
    /// Название модели автомобиля
    /// </summary>
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// Класс автомобиля (Economy, Business, Premium)
    /// </summary>
    public string ModelClass { get; set; } = string.Empty;

    /// <summary>
    /// Стоимость аренды в час
    /// </summary>
    public decimal RentalPricePerHour { get; set; }
}

/// <summary>
/// Data Transfer Object для создания нового автомобиля
/// </summary>
public class CreateCarDto
{
    /// <summary>
    /// Государственный номерной знак автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Цвет автомобиля
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Идентификатор поколения модели автомобиля
    /// </summary>
    public required int ModelGenerationId { get; set; }
}

/// <summary>
/// Data Transfer Object для автомобиля с количеством аренд
/// </summary>
public class CarWithRentalCountDto
{
    /// <summary>
    /// Данные автомобиля
    /// </summary>
    public CarDto Car { get; set; } = null!;

    /// <summary>
    /// Количество аренд данного автомобиля
    /// </summary>
    public int RentalCount { get; set; }
}