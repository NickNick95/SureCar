using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using serviceModel = SureCar.Services.Models;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<serviceModel.Warehouse>), 200)]
        public IActionResult GetAll()
        {
            var result = _warehouseService.GetAll();

            return Ok(new ResponseResult<List<serviceModel.Warehouse>>
            {
                IsSuccessful = true,
                Content = result
            });
        }
    }
}
