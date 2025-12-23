using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTO;

/// <summary>
/// DTO для поколения модели
/// </summary>
public class ModelGenerationDto
{
    /// <summary>
    /// Год выпуска
    /// </summary>
    public required int Year { get; set; }
    
    /// <summary>
    /// Объем двигателя
    /// </summary>
    public required double EngineVolume { get; set; }
    
    /// <summary>
    /// Тип коробки передач
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required TransmissionType TransmissionType { get; set; }
    
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int ModelId { get; set; }
    
    /// <summary>
    /// Стоимость аренды в час
    /// </summary>
    public required decimal RentalCostPerHour { get; set; }
}