using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Models;
using OrderProcessingSystem.Services;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProducer _orderProducer;

        public OrdersController(IOrderProducer orderProducer)
        {
            _orderProducer = orderProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            await _orderProducer.ProduceOrderAsync(orderRequest);
            return Accepted("Order submitted successfully.");
        }
    }
}
