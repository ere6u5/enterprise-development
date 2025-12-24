using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.Seeder;
using System.Text.Json;
using System.Text;
using Polly;
using Polly.Extensions.Http;

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
                    .AddPolicyHandler(GetRetryPolicy());
            })
            .ConfigureLogging(logging =>
            {
                logging.AddConsole();
            })
            .Build();

        await host.RunAsync();
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}