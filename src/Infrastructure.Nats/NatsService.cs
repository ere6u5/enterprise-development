using System.Text.Json;
using Application.Service;
using NATS.Client;

namespace Infrastructure.Nats;

public class NatsService : INatsService
{
    private readonly IConnection _connection;
    private readonly ILogger<NatsService> _logger;

    public NatsService(string natsUrl, ILogger<NatsService> logger)
    {
        _logger = logger;
        
        var opts = ConnectionFactory.GetDefaultOptions();
        opts.Url = natsUrl;
        
        _connection = new ConnectionFactory().CreateConnection(opts);
    }

    public async Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours)
    {
        try
        {
            var message = new
            {
                EventType = "RentalCreated",
                RentalId = rentalId,
                CarId = carId,
                ClientId = clientId,
                RentalStart = rentalStart,
                RentalHours = rentalHours,
                Timestamp = DateTime.UtcNow
            };

            var jsonMessage = JsonSerializer.Serialize(message);
            _connection.Publish("rentals.created", Encoding.UTF8.GetBytes(jsonMessage));
            
            _logger.LogInformation("Published RentalCreated event for rental {RentalId}", rentalId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing RentalCreated event");
        }
    }

    public async Task PublishRentalEndedAsync(int rentalId, DateTime endTime)
    {
        try
        {
            var message = new
            {
                EventType = "RentalEnded",
                RentalId = rentalId,
                EndTime = endTime,
                Timestamp = DateTime.UtcNow
            };

            var jsonMessage = JsonSerializer.Serialize(message);
            _connection.Publish("rentals.ended", Encoding.UTF8.GetBytes(jsonMessage));
            
            _logger.LogInformation("Published RentalEnded event for rental {RentalId}", rentalId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing RentalEnded event");
        }
    }
}