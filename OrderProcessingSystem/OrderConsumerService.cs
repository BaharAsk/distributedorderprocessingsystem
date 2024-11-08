using Microsoft.Extensions.Hosting;
using OrderProcessingSystem.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Api
{
    public class OrderConsumerService : BackgroundService
    {
        private readonly IOrderConsumer _orderConsumer;

        public OrderConsumerService(IOrderConsumer orderConsumer)
        {
            _orderConsumer = orderConsumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Start consuming orders
            _orderConsumer.ConsumeOrder();

            return Task.CompletedTask;
        }
    }
}

