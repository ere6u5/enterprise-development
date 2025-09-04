namespace CarRental.Domain.Model;

public class Rental
{
    public int Id { get; set; }
    public required int CarId { get; set; }
    public required Car Car { get; set; }
    public required int CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public required DateTime RentalStart { get; set; }
    public required int RentalHours { get; set; }
    public DateTime RentalEnd => RentalStart.AddHours(RentalHours);
}