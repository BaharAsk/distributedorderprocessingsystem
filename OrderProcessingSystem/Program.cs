using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderProcessingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Kafka Producer
builder.Services.AddSingleton<IProducer<Null, string>>(provider =>
{
    var config = new ProducerConfig { BootstrapServers = builder.Configuration["Kafka:BootstrapServers"] ?? "localhost:9092" };
    return new ProducerBuilder<Null, string>(config).Build();
});

// Register OrderProducer service
builder.Services.AddSingleton<IOrderProducer, OrderProducer>();

// Register Swagger (for API documentation)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification System API", Version = "v1" });
});

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification System API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run the application
app.Run();
