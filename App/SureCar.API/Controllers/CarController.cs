using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetOne()
        {
           return Ok(string.Empty);
        }
    }
}
