using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using serviceModel = SureCar.Services.Models;

namespace SureCar.API.Controllers
{
    /// <summary>
    /// Controller for working with warehouse
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="warehouseService">The warehouse service</param>
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Gets all
        /// </summary>
        /// <returns>List of warehouse</returns>
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
