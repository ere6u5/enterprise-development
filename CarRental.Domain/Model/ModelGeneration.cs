using CarRental.Domain.Enums;

namespace CarRental.Domain.Model;

public class ModelGeneration
{
    public int Id { get; set; }
    public required int ModelId { get; set; }
    public required CarModel Model { get; set; }
    public required int ReleaseYear { get; set; }
    public required decimal EngineVolume { get; set; }
    public required TransmissionType Transmission { get; set; }
    public required decimal RentalCostPerHour { get; set; }
}