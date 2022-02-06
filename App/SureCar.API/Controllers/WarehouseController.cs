using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using SureCar.Services.Models;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WarehouseController : Controller
    {

        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Warehouse>), 200)]
        public IActionResult GetAll()
        {
            var result = _warehouseService.GetAll().ToList();

            return Json(new ResponseResult<List<Warehouse>>
            {
                Content = result
            });
        }
    }
}
