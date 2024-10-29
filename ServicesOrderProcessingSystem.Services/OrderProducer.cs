using Confluent.Kafka;
using Newtonsoft.Json;
using System.Threading.Tasks;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Services
{
    public class OrderProducer : IOrderProducer
    {
        private readonly IProducer<Null, string> _kafkaProducer;

        public OrderProducer(IProducer<Null, string> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task ProduceOrderAsync(OrderRequest order)
        {
            var message = new Message<Null, string> { Value = JsonConvert.SerializeObject(order) };
            await _kafkaProducer.ProduceAsync("order_topic", message);
        }
    }
}
