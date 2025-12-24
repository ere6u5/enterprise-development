using Microsoft.Extensions.Hosting;

// var builder = DistributedApplication.CreateBuilder(args);
var builder = DistributedApplication.CreateBuilder(args);

// var builder = DistributedApplication.CreateBuilder(new DistributedApplicationOptions
// {
//     Args = args,
//     DisableDashboard = true
// });

// Добавляем MySQL базу данных
var mysql = builder.AddMySql("mysql")
    .AddDatabase("carrentaldb");

// Добавляем NATS
var nats = builder.AddNats("nats");

// Добавляем API проект
var api = builder.AddProject<Projects.Api>("api")
    .WithReference(mysql)
    .WithReference(nats)
    .WaitFor(mysql) // Ждем пока база поднимется
    .WaitFor(nats); // Ждем пока NATS поднимется

builder.Build().Run();