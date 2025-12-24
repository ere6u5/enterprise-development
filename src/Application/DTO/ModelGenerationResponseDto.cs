using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dto;

/// <summary>
/// Response Dto для поколения модели
/// </summary>
public class ModelGenerationResponseDto
{
    /// <summary>
    /// Идентификатор поколения
    /// </summary>
    public required int Id { get; set; }
    
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