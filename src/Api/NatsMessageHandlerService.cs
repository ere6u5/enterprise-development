using System.Text;
using System.Text.Json;
using Application.Service;
using NATS.Client;

namespace Api;

/// <summary>
/// Служба для обработки сообщений из NATS
/// </summary>
public class NatsMessageHandlerService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NatsMessageHandlerService> _logger;
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IAsyncSubscription? _subscription;

    /// <summary>
    /// Инициализирует новый экземпляр службы обработки сообщений NATS
    /// </summary>
    /// <param name="serviceProvider">Провайдер служб</param>
    /// <param name="logger">Логгер</param>
    /// <param name="configuration">Конфигурация</param>
    public NatsMessageHandlerService(
        IServiceProvider serviceProvider,
        ILogger<NatsMessageHandlerService> logger,
        IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Запускает службу обработки сообщений NATS
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача запуска службы</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("NATS Message Handler starting...");
        
        // Создаем соединение в отдельном потоке
        Task.Run(() => ConnectAndSubscribe(cancellationToken), cancellationToken);
        
        return Task.CompletedTask;
    }

    private void ConnectAndSubscribe(CancellationToken cancellationToken)
    {
        try
        {
            var opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = _configuration["Nats:Url"] ?? "nats://localhost:4222";
            opts.MaxReconnect = 10;
            opts.ReconnectWait = 1000;
            
            _connection = new ConnectionFactory().CreateConnection(opts);
            _logger.LogInformation("Connected to NATS for message handling");

            // Подписываемся на сообщения о генерации аренд
            _subscription = _connection.SubscribeAsync("rentals.generated", (sender, args) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(args.Message.Data);
                    _logger.LogDebug("Received NATS message: {Message}", message);
                    
                    var json = JsonSerializer.Deserialize<JsonElement>(message);
                    
                    if (json.TryGetProperty("EventType", out var eventType) && 
                        eventType.GetString() == "RentalGenerated" &&
                        json.TryGetProperty("Data", out var data))
                    {
                        // Обрабатываем в отдельной задаче
                        Task.Run(() => ProcessRentalGeneration(data, cancellationToken), cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing NATS message");
                }
            });

            _subscription.Start();
            _logger.LogInformation("Subscribed to rentals.generated topic");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to NATS");
        }
    }

    private async Task ProcessRentalGeneration(JsonElement data, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        try
        {
            var rentalDto = data.Deserialize<Application.Dto.RentalDto>();
            if (rentalDto != null)
            {
                var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();
                var rentalId = await rentalService.CreateRentalAsync(rentalDto);
                
                _logger.LogInformation("Processed generated rental with ID {RentalId}", rentalId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing rental generation");
        }
    }

    /// <summary>
    /// Останавливает службу обработки сообщений NATS
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача остановки службы</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _subscription?.Unsubscribe();
        _subscription?.Dispose();
        _connection?.Close();
        
        _logger.LogInformation("NATS Message Handler stopped");
        return Task.CompletedTask;
    }
}