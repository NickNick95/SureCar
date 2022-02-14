using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SureCar.API.Controllers;
using SureCar.API.Models.Order;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using System.Collections.Generic;
using Xunit;
using serviceModel = SureCar.Services.Models;
using orderModel = SureCar.Services.Models.OrderModel;

namespace SureCar.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void CreateOrder_OrderModel_Success()
        {
            // Arrange
            var orderId = 1;
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(x =>
                x.CreateOrder(It.IsAny<serviceModel.Order>()))
                .Returns(orderId);
            var mapperMock = new Mock<IMapper>();
            var controller = new OrderController(orderServiceMock.Object, mapperMock.Object);

            var order = new Order
            {
                UserId = "1",
                VehicleIds = new List<int> { 1 }
            };

            // Act
            var result = controller.CreateOrder(order);

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as ResponseResult<ResponseMessage>;

            Assert.NotNull(response);
            Assert.True(response.IsSuccessful);
            Assert.NotNull(response.Content.Message);
        }

        [Fact]
        public void OrderModel_UserIdIsNull_Success()
        {
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(x =>
                x.CreateOrder(It.IsAny<serviceModel.Order>()));
            var mapperMock = new Mock<IMapper>();
            var controller = new OrderController(orderServiceMock.Object, mapperMock.Object);

            var order = new Order
            {
                UserId = null,
                VehicleIds = new List<int> { 1 }
            };

            // Act
            var result = controller.CreateOrder(order);

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as ResponseResult<ResponseMessage>;
            Assert.NotNull(response);
            Assert.NotNull(response.Content.Message);
            Assert.False(response.IsSuccessful);
        }

        [Fact]
        public void OrderModel_VehicleIdsIsNull_Success()
        {
            // Arrange
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(x =>
                x.CreateOrder(It.IsAny<serviceModel.Order>()));
            var mapperMock = new Mock<IMapper>();
            var controller = new OrderController(orderServiceMock.Object, mapperMock.Object);

            var order = new Order
            {
                UserId = "1",
                VehicleIds = null
            };

            // Act
            var result = controller.CreateOrder(order);

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as ResponseResult<ResponseMessage>;
            Assert.NotNull(response);
            Assert.NotNull(response.Content.Message);
            Assert.False(response.IsSuccessful);
        }

        [Fact]
        public void OrderModel_Success()
        {
            // Arrange
            var orderDetails = new List<orderModel.OrderDetails>
            {
                new orderModel.OrderDetails
                {
                    Id = 1,
                    Email = "test@gmail.com",
                    PhoneNumber = "325841",
                    UserId = "1",
                    UserName = "userTestOne",
                    Vehicles = new List<serviceModel.Vehicle>
                    {
                        new serviceModel.Vehicle
                        {
                            Id =1,
                            DateAdded = System.DateTime.UtcNow,
                            Licensed = true,
                            Make = "Toyota",
                            Model = "Rav4",
                            Price = 35000,
                            YearModel = 2020
                        }
                    }
                }
            };

            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(x =>
                x.GetAll())
                .Returns(orderDetails);
            var mapperMock = new Mock<IMapper>();
            var controller = new OrderController(orderServiceMock.Object, mapperMock.Object);


            // Act
            var result = controller.GetOrders();

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);


            var response = okObjectResult.Value as ResponseResult<List<orderModel.OrderDetails>>;
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.IsSuccessful);
        }
    }
}
