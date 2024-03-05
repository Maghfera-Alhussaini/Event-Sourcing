using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Infrastructure.MessageBus;
using Anis.TrainingProject.Infrastructure.Persistance;
using Anis.TrainingProject.Services;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventStore, EventStore>();
// Configure the HTTP request pipeline.
app.MapGrpcService<InvitationsService>();
builder.Services.AddSingleton(new ServiceBusClient(""));
builder.Services.AddSingleton<ServiceBusPublisher>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
