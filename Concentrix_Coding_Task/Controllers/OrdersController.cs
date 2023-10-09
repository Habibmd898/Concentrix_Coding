#nullable disable
using Concentrix_Coding_Task.Dtos;
using Microsoft.AspNetCore.Mvc;
using Concentrix_Coding_Task.RabbitMQ;
using Concentrix_Producer.Services.APIManager;
using Concentrix__Common.Modal;
using Microsoft.AspNetCore.Authorization;

namespace Concentrix_Coding_Task.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;
        private readonly IOrdersAPI _ordersAPI;

        public OrdersController(IMessageProducer messagePublisher, IOrdersAPI ordersAPI)
        {
            _messagePublisher = messagePublisher;
            _ordersAPI = ordersAPI;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            _messagePublisher.SendMessage(orderDto);

            return Ok();
        }

        [HttpGet(Name = "GetAllOrders")]

        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _ordersAPI.GetOrders(new PagingParams(pageNumber, pageSize)));
        }

    }
}
