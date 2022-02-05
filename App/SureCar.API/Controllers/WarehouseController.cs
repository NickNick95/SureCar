using Microsoft.AspNetCore.Mvc;
using SureCar.Services.Interface;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : Controller
    {

        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _warehouseService.GetAll().ToList();

           return Ok(result);
        }
    }
}
