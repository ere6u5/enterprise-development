using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Поколение модели автомобиля (справочник)
/// </summary>
public class ModelGeneration
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
    public required TransmissionType TransmissionType { get; set; }
    
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    public required int ModelId { get; set; }
    
    /// <summary>
    /// Модель автомобиля
    /// </summary>
    public required CarModel Model { get; set; }
    
    /// <summary>
    /// Стоимость аренды в час
    /// </summary>
    public required decimal RentalCostPerHour { get; set; }
}