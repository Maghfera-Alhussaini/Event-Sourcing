using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Infrastructure.MessageBus;
using Anis.TrainingProject.Infrastructure.Persistance;
using Anis.TrainingProject.Services;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddMediatR(x =>x.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus")));
builder.Services.AddSingleton<ServiceBusPublisher>();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapGrpcService<InvitationsService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
