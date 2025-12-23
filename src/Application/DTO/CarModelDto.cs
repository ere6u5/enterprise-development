using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTO;

/// <summary>
/// DTO для модели автомобиля
/// </summary>
public class CarModelDto
{
    /// <summary>
    /// Название модели
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Тип привода
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required CarDriveType DriveType { get; set; }
    
    /// <summary>
    /// Число посадочных мест
    /// </summary>
    public required int SeatCount { get; set; }
    
    /// <summary>
    /// Тип кузова
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required BodyType BodyType { get; set; }
    
    /// <summary>
    /// Класс автомобиля
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required CarClass CarClass { get; set; }
}