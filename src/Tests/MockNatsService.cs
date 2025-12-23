using Application.Service;

namespace Tests;

public class MockNatsService : INatsService
{
    public Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours)
    {
        return Task.CompletedTask;
    }

    public Task PublishRentalEndedAsync(int rentalId, DateTime endTime)
    {
        return Task.CompletedTask;
    }
}