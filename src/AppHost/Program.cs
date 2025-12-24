using Microsoft.Extensions.Hosting;

// var builder = DistributedApplication.CreateBuilder(args);
var builder = DistributedApplication.CreateBuilder(args);

// Добавляем MySQL базу данных
var mysql = builder.AddMySql("mysql")
    .AddDatabase("carrentaldb");

// Добавляем NATS
var nats = builder.AddNats("nats");

// Добавляем API проект
var api = builder.AddProject<Projects.Api>("api")
    .WithReference(mysql)
    .WithReference(nats);

// Добавляем Generator проект
var generator = builder.AddProject<Projects.Generator>("generator")
    .WithReference(nats);

builder.Build().Run();