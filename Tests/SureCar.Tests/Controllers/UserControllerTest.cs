using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SureCar.API.Controllers;
using SureCar.API.Models.Response;
using SureCar.API.Models.User;
using SureCar.Services.Interface;
using System.Threading.Tasks;
using Xunit;
using userModel = SureCar.Services.Models.UserModels;

namespace SureCar.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task RegisterUser_UserForRegistrationModel_Success()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x =>
                x.CreateUserAsync(It.IsAny<userModel.User>()))
                .ReturnsAsync(true);
            var mapperMock = new Mock<IMapper>();
            var controller = new UserController(mapperMock.Object, userServiceMock.Object);
            var userForRegistration = getUserForRegistration();

            // Act
            var result = await controller.RegisterUser(userForRegistration);

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as ResponseResult<ResponseMessage>;

            Assert.NotNull(response);
            Assert.True(response.IsSuccessful);
            Assert.NotNull(response.Content.Message);
        }

        [Fact]
        public async Task RegisterUser_UserForRegistrationModel_UserNameIsEmpty_Success()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x =>
                x.CreateUserAsync(It.IsAny<userModel.User>()))
                .ReturnsAsync(false);
            var mapperMock = new Mock<IMapper>();
            var controller = new UserController(mapperMock.Object, userServiceMock.Object);
            var userForRegistration = getUserForRegistration();
            userForRegistration.UserName = string.Empty;

            // Act
            var result = await controller.RegisterUser(userForRegistration);

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as ResponseResult<ResponseMessage>;

            Assert.NotNull(response);
            Assert.False(response.IsSuccessful);
            Assert.NotNull(response.Content.Message);
        }

        private UserForRegistration getUserForRegistration()
        {
            return new UserForRegistration
            {
                UserName = "TestName",
                Email = "email@gmail.com",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "032417452",
                Password = "1234",
                ConfirmPassword = "1234"
            };
        }
    }
}
