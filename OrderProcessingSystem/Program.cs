var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public void ConfigureServices(IServiceCollection services)
{
    // Register controllers
    services.AddControllers();

    // Register services from OrderProcessingSystem.Services
    services.AddSingleton<IOrderProducer, OrderProducer>(); // Kafka producer example
    services.AddSingleton<IOrderRepository, OrderRepository>(); // SQL repository example

    // Add messaging services
    services.AddSingleton<IKafkaProducer, KafkaProducer>();
    services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

    // Configure Redis
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = Configuration.GetConnectionString("RedisConnection");
        options.InstanceName = "OrderProcessing_";
    });

    // Configure SQL Server
    services.AddDbContext<OrderDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    // Swagger for API documentation (optional)
    services.AddSwaggerGen();


}

