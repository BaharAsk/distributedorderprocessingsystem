using System.Threading.Tasks;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Services
{
    public interface IOrderProducer
    {
        Task ProduceOrderAsync(OrderRequest order);
    }
}
