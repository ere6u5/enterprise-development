namespace Application.Service;

/// <summary>
/// Interface for NATS messaging service
/// </summary>
public interface INatsService
{
    /// <summary>
    /// Publishes rental created event
    /// </summary>
    /// <param name="rentalId">Rental ID</param>
    /// <param name="carId">Car ID</param>
    /// <param name="clientId">Client ID</param>
    /// <param name="rentalStart">Rental start time</param>
    /// <param name="rentalHours">Rental duration in hours</param>
    Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours);
    
    /// <summary>
    /// Publishes rental ended event
    /// </summary>
    /// <param name="rentalId">Rental ID</param>
    /// <param name="endTime">End time</param>
    Task PublishRentalEndedAsync(int rentalId, DateTime endTime);
}