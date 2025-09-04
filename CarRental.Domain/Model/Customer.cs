namespace CarRental.Domain.Model;

public class Customer
{
    public int Id { get; set; }
    public required string DriverLicenseNumber { get; set; }
    public required string FullName { get; set; }
    public required DateOnly BirthDate { get; set; }
}