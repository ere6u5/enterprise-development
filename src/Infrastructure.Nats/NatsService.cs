using System.Text;
using System.Text.Json;
using Application.Service;
using Microsoft.Extensions.Logging;
using NATS.Client;
using Polly;

namespace Infrastructure.Nats;

/// <summary>
/// Сервис для работы с NATS (системой обмена сообщениями)
/// </summary>
public class NatsService : INatsService
{
    private IConnection? _connection;
    private readonly ILogger<NatsService> _logger;
    private readonly string _natsUrl;
    private readonly AsyncPolicy _retryPolicy;

    /// <summary>
    /// Конструктор сервиса NATS
    /// </summary>
    public NatsService(string natsUrl, ILogger<NatsService> logger)
    {
        _logger = logger;
        _natsUrl = natsUrl;

        // Настройка политики ретраев с экспоненциальной задержкой
        _retryPolicy = Policy
            .Handle<NATSException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        exception,
                        "Retry {RetryCount} for NATS connection after {TimeSpan} seconds",
                        retryCount, timeSpan.TotalSeconds);
                });

        InitializeConnection();
    }

    /// <summary>
    /// Инициализация подключения к NATS
    /// </summary>
    private void InitializeConnection()
    {
        _retryPolicy.ExecuteAsync(async () =>
        {
            await Task.Run(() =>
            {
                var opts = ConnectionFactory.GetDefaultOptions();
                opts.Url = _natsUrl;
                opts.MaxReconnect = 10; // Максимальное количество переподключений
                opts.ReconnectWait = 1000; // Ждать 1 секунду между попытками
                opts.AsyncErrorEventHandler = (sender, args) =>
                {
                    _logger.LogError(args.Error, "NATS async error");
                };
                opts.ReconnectedEventHandler = (sender, args) =>
                {
                    _logger.LogInformation("Reconnected to NATS");
                };
                opts.ClosedEventHandler = (sender, args) =>
                {
                    _logger.LogInformation("NATS connection closed");
                };

                _connection = new ConnectionFactory().CreateConnection(opts);
                _logger.LogInformation("Connected to NATS at {NatsUrl}", _natsUrl);
            });
        }).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Публикация события создания аренды
    /// </summary>
    public async Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours)
    {
        await _retryPolicy.ExecuteAsync(() => Task.Run(() =>
        {
            try
            {
                if (_connection == null || _connection.State != ConnState.CONNECTED)
                {
                    throw new InvalidOperationException("NATS connection is not initialized or connected");
                }

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
                throw;
            }
        }));
    }

    /// <summary>
    /// Публикация события завершения аренды
    /// </summary>
    public async Task PublishRentalEndedAsync(int rentalId, DateTime endTime)
    {
        await _retryPolicy.ExecuteAsync(() => Task.Run(() =>
        {
            try
            {
                if (_connection == null || _connection.State != ConnState.CONNECTED)
                {
                    throw new InvalidOperationException("NATS connection is not initialized or connected");
                }

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
                throw;
            }
        }));
    }
}