namespace Contracts;

public record RentalCreatedMessage(
    int RentalId,
    int CarId,
    int ClientId,
    DateTime RentalStart,
    int RentalHours,
    DateTime Timestamp);

public record RentalEndedMessage(
    int RentalId,
    DateTime EndTime,
    DateTime Timestamp);

public record RentalGeneratedMessage(
    int CarId,
    int ClientId,
    DateTime RentalStart,
    int RentalHours);