using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Response;
using SureCar.API.Models.User;
using SureCar.Services.Interface;
using SureCar.Services.Models;
using serviceModel = SureCar.Services.Models;
using apiModels = SureCar.API.Models.User;

namespace SureCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistration model)
        {
            var user = _mapper.Map<serviceModel.User>(model);
            var result = await _userService.CreateUserAsync(user);

            var response = new ResponseResult<ResponseMessage>();

            if (result)
            {
                response.Content = new ResponseMessage("Success");
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] apiModels.UserLogin model)
        {
            var userLogin = _mapper.Map<serviceModel.UserLogin>(model);

            var applicationUser = await _userService.GetUserByNameAsync(userLogin.UserName);
            if (applicationUser == null)
                return Unauthorized(new ResponseResult<ResponseMessage> {
                    IsSuccessful = false,
                    ErrorMessage = "The account is not found",
                });

            var token = await _userService.LoginAsync(applicationUser, userLogin.Password);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new ResponseResult<ResponseMessage>
                {
                    IsSuccessful = false,
                    ErrorMessage = "The account has a problem. Contact administrator to get more details",
                });

            var user = _mapper.Map<serviceModel.User>(applicationUser);

            return Ok(new ResponseResult<User>()
            {
                Token = token,
                Content = user
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] UserLogout userLogout)
        {
            if (string.IsNullOrEmpty(userLogout.UserId))
                return BadRequest("User id is required");

            var user = await _userService.GetUserByIdAsync(userLogout.UserId);
            if (user == null)
                return NotFound(new ResponseResult<ResponseMessage>
                {
                    IsSuccessful = false,
                    ErrorMessage = "The account is not found",
                });

            await _userService.LogoutAsync(user);

            return Ok(new ResponseResult<ResponseMessage>
            {
                IsSuccessful = true
            }); ;
        }
    }
}
