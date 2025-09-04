using CarRental.Domain.Enums;

namespace CarRental.Domain.Model;

public class CarModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required WheelDriveType DriveType { get; set; }
    public required int SeatsCount { get; set; }
    public required BodyType BodyType { get; set; }
    public required CarClass Class { get; set; }
}