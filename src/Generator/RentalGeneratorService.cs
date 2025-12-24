using System.Text;
using System.Text.Json;
using Bogus;
using Domain.Seeder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client;
using Polly;

namespace Generator;

public class RentalGeneratorService : IHostedService
{
    private readonly HttpClient _httpClient;
    private readonly DataSeeder _seeder;
    private readonly ILogger<RentalGeneratorService> _logger;
    private Timer? _timer;
    private readonly string _apiUrl;
    private IConnection? _natsConnection;
    private readonly string _natsUrl;
    private readonly Faker _faker = new();

    public RentalGeneratorService(
        HttpClient httpClient,
        DataSeeder seeder,
        ILogger<RentalGeneratorService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _seeder = seeder;
        _logger = logger;
        _apiUrl = configuration["Api:Url"] ?? "http://localhost:5000";
        _natsUrl = configuration["Nats:Url"] ?? "nats://localhost:4222";
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rental Generator Service started");

        // Подключаемся к NATS
        ConnectToNats();

        // Генерируем аренды каждые 30 секунд
        _timer = new Timer(GenerateRentals, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

        return Task.CompletedTask;
    }

    private void ConnectToNats()
    {
        try
        {
            var opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = _natsUrl;
            opts.MaxReconnect = 10;
            opts.ReconnectWait = 1000;

            _natsConnection = new ConnectionFactory().CreateConnection(opts);
            _logger.LogInformation("Connected to NATS at {NatsUrl}", _natsUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to NATS");
        }
    }

    private async void GenerateRentals(object? state)
    {
        try
        {
            var random = new Random();
            var count = random.Next(3, 6);

            _logger.LogInformation("Generating {Count} rentals", count);

            for (var i = 0; i < count; i++)
            {
                // Генерируем случайные данные
                var rentalDto = new
                {
                    CarId = _faker.Random.Int(1, 12),
                    ClientId = _faker.Random.Int(1, 10),
                    RentalStart = _faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                    RentalHours = _faker.Random.Int(1, 72)
                };

                // Отправляем через NATS
                if (_natsConnection != null && _natsConnection.State == ConnState.CONNECTED)
                {
                    var json = JsonSerializer.Serialize(new
                    {
                        EventType = "RentalGenerated",
                        Data = rentalDto,
                        Timestamp = DateTime.UtcNow
                    });

                    _natsConnection.Publish("rentals.generated",
                        Encoding.UTF8.GetBytes(json));

                    _logger.LogDebug("Published rental to NATS: Car {CarId}, Client {ClientId}",
                        rentalDto.CarId, rentalDto.ClientId);
                }
                else
                {
                    // Fallback: отправляем через HTTP если NATS не доступен
                    var json = JsonSerializer.Serialize(rentalDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"{_apiUrl}/rental", content);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogDebug("Created rental via HTTP: Car {CarId}, Client {ClientId}",
                            rentalDto.CarId, rentalDto.ClientId);
                    }
                }

                await Task.Delay(100);
            }

            _logger.LogInformation("Successfully generated {Count} rentals", count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating rentals");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        _natsConnection?.Close();
        _logger.LogInformation("Rental Generator Service stopped");
        return Task.CompletedTask;
    }
}