namespace Application.Service;

public interface INatsService
{
    Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours);
    Task PublishRentalEndedAsync(int rentalId, DateTime endTime);
}