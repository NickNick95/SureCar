using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Order;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using serviceModel = SureCar.Services.Models;
using orderModel = SureCar.Services.Models.OrderModel;
using userModel = SureCar.Services.Models.UserModels;

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
        [Authorize]
        public IActionResult CreateOrder([FromBody] Order model)
        {
            var order = _mapper.Map<serviceModel.Order>(model);
            var result = _orderService.CreateOrder(order);

            var response = new ResponseResult<ResponseMessage>();
            if (result.HasValue)
            {
                response.Content = new ResponseMessage($"{result}");
                response.IsSuccessful = true;

                return Ok(response);
            }
            else
            {
                response.Content = new ResponseMessage("Failded");
                response.IsSuccessful = false;

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Authorize(Roles = userModel.UserRoles.Administrator)]
        public IActionResult GetOrders()
        {
            var result = _orderService.GetAll();

            return Ok(new ResponseResult<List<orderModel.OrderDetails>>()
            {
                IsSuccessful = true,
                Content = result
            });
        }
    }
}
