using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderProcessingSystem.Messaging
{
    public class OrderConsumer : IOrderConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public OrderConsumer()
        {
            // Setup the RabbitMQ connection and channel
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare the queue (ensure the queue exists)
            _channel.QueueDeclare(queue: "order_queue",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void ConsumeOrder()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Process the message
                Console.WriteLine("Received Order: " + message);

                // TODO: Add any additional processing logic here, such as:
                // - Parsing the order JSON into an Order object
                // - Saving to a database
                // - Sending notifications, etc.
            };

            // Start consuming messages from the queue
            _channel.BasicConsume(queue: "order_queue",
                                  autoAck: true,
                                  consumer: consumer);
        }
    }
}



