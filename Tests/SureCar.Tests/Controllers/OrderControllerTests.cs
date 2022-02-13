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

namespace SureCar.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void CreateOrderWithOrderModel()
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

            var expectedOrderId = okObjectResult.Value as ResponseResult<ResponseMessage>;
            Assert.NotNull(expectedOrderId);

          //  Assert.Equal(orderId, expectedOrderId);
        }
    }
}
