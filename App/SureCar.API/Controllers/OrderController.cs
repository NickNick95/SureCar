using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Order;
using SureCar.Services.Interface;
using serviceModeel = SureCar.Services.Models;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = serviceModeel.UserRole.Administrator)]
        public IActionResult GetOrders()
        {
            return Ok();
        }
    }
}
