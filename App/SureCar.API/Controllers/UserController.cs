using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SureCar.API.Models.Response;
using SureCar.API.Models.User;
using SureCar.Services.Interface;
using apiModels = SureCar.API.Models.User;
using userModel = SureCar.Services.Models.UserModels;

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

        [HttpGet("isadmin/{userId}")]
        public async Task<IActionResult> CheckIsAdministratorUser([FromRoute] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required");

            var user = await _userService.GetUserByIdAsync(userId);
            var result = await _userService.IsUserAdmin(user);

            return Ok(new ResponseResult<ResponseFlag>()
            {
                IsSuccessful = true,
                Content = new ResponseFlag(result)
            });
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistration model)
        {
            var user = _mapper.Map<userModel.User>(model);
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
                response.Errors = _userService.GetErrors();
                response.IsSuccessful = false;

                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] apiModels.UserLogin model)
        {
            var userLogin = _mapper.Map<userModel.UserLogin>(model);

            var result = await _userService.LoginAsync(userLogin);

            if (result == null)
                return Ok(new ResponseResult<userModel.User>
                {
                    IsSuccessful = false,
                    Errors = _userService.GetErrors()
                });

            return Ok(new ResponseResult<userModel.User>()
            {
                IsSuccessful = true,
                Token = result.Token,
                Content = result.User
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
                return Ok(new ResponseResult<ResponseMessage>
                {
                    IsSuccessful = false,
                    ErrorMessage = "The account is not found",
                });

            var result = await _userService.LogoutAsync(user);

            if (result)
                return Ok(new ResponseResult<ResponseMessage>
                {
                    IsSuccessful = false,
                    Errors = _userService.GetErrors()
                });
            else
                return Ok(new ResponseResult<ResponseMessage>
                {
                    IsSuccessful = true
                }); ;
        }
    }
}
