namespace CarRental.Domain.Model;

public class Car
{
    public int Id { get; set; }
    public required int GenerationId { get; set; }
    public required ModelGeneration Generation { get; set; }
    public required string LicensePlate { get; set; }
    public required string Color { get; set; }
}