using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Добавляем MySQL базу данных
var mysql = builder.AddMySql("mysql")
    .WithVolumeMount("mysql-data", "/var/lib/mysql", VolumeMountType.Named)
    .AddDatabase("carrentaldb");

// Добавляем NATS
var nats = builder.AddNats("nats");

// Добавляем API проект
var api = builder.AddProject<Projects.Api>("api")
    .WithReference(mysql)
    .WithReference(nats)
    .WaitFor(mysql);

// Добавляем генератор
var generator = builder.AddProject<Projects.Generator>("generator")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();