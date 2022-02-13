using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SureCar.API.Controllers;
using SureCar.API.Models.Response;
using SureCar.Services.Interface;
using System.Collections.Generic;
using Xunit;
using serviceModel = SureCar.Services.Models;

namespace SureCar.Tests.Controllers
{
    public class WarehouseControllerTest
    {
        [Fact]
        public void GetAll_Success()
        {
            // Arrange
            var warehouses = GetWarehouses();

            var warehouseServiceMock = new Mock<IWarehouseService>();
            warehouseServiceMock.Setup(x =>
                x.GetAll())
                .Returns(warehouses);
            var controller = new WarehouseController(warehouseServiceMock.Object);

            // Act
            var result = controller.GetAll();

            var okObjectResult = result as OkObjectResult;

            // Arrange
            Assert.NotNull(okObjectResult);


            var response = okObjectResult.Value as ResponseResult<List<serviceModel.Warehouse>>;
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.True(response.IsSuccessful);
        }

        private List<serviceModel.Warehouse> GetWarehouses()
        {
            return new List<serviceModel.Warehouse>()
            {
                new serviceModel.Warehouse
                {
                    Id = 1,
                    Name = "Warehouse A",
                    Location = new serviceModel.Location
                    {
                        Latitude = 12,
                        Longitude = 15
                    },
                     Car = new serviceModel.Car
                     {
                         Id=1,
                         Location = "test location",
                         Vehicles = new List<serviceModel.Vehicle>
                         {
                             new serviceModel.Vehicle
                             {
                                 Id = 1,
                                 Make = "Toyota",
                                 Price = 35000,
                                 Licensed = true,
                                 Model = "Rav4",
                                 YearModel = 2020
                             }
                         }
                     }
                }
            };
        }
    }
}
