using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.Seeder;
using System.Text.Json;
using System.Text;
namespace Generator;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<RentalGeneratorService>();
                services.AddSingleton<DataSeeder>();
                services.AddHttpClient<RentalGeneratorService>()
                    .AddTransientHttpErrorPolicy(policy => 
                        policy.WaitAndRetryAsync(3, retryAttempt => 
                            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
            })
            .ConfigureLogging(logging =>
            {
                logging.AddConsole();
            })
            .Build();

        await host.RunAsync();
    }
}

public class RentalGeneratorService : IHostedService
{
    private readonly HttpClient _httpClient;
    private readonly DataSeeder _seeder;
    private readonly ILogger<RentalGeneratorService> _logger;
    private Timer? _timer;  // Добавьте ? чтобы сделать nullable
    private readonly string _apiUrl;

    public RentalGeneratorService(
        HttpClient httpClient,
        DataSeeder seeder,
        ILogger<RentalGeneratorService> logger,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _seeder = seeder;
        _logger = logger;
        _apiUrl = configuration["Api:Url"] ?? "https://localhost:5001";
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Rental Generator Service started");

        // Генерируем аренды каждые 30 секунд через REST API
        _timer = new Timer(GenerateRentals, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

        return Task.CompletedTask;
    }

    private async void GenerateRentals(object? state)
    {
        try
        {
            var random = new Random();
            var rentals = _seeder.Rentals;

            // Генерируем 3-5 случайных аренд
            var count = random.Next(3, 6);

            _logger.LogInformation("Generating {Count} rentals", count);

            for (var i = 0; i < count; i++)
            {
                var rental = rentals[random.Next(rentals.Count)];

                var rentalDto = new
                {
                    CarId = rental.CarId,
                    ClientId = rental.ClientId,
                    RentalStart = rental.RentalStart,
                    RentalHours = rental.RentalHours
                };

                // Заменяем PostAsJsonAsync на стандартный HTTP POST
                var json = JsonSerializer.Serialize(rentalDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiUrl}/rental", content);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogDebug("Created rental for car {CarId} and client {ClientId}",
                        rental.CarId, rental.ClientId);
                }
                else
                {
                    _logger.LogWarning("Failed to create rental. Status: {StatusCode}", 
                        response.StatusCode);
                }

                // Небольшая задержка между отправками
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
        _logger.LogInformation("Rental Generator Service stopped");
        return Task.CompletedTask;
    }
}