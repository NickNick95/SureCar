﻿using AutoMapper;
using SureCar.Repositories.Interfaces;
using SureCar.Services.Interface;
using SureCar.Services.Models;
using entitieModel = SureCar.Entities;
using orderModel = SureCar.Services.Models.OrderModel;

namespace SureCar.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepository<entitieModel.VehicleOrder> _vehicleOrderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
            IRepository<entitieModel.VehicleOrder> vehicleOrderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _vehicleOrderRepository = vehicleOrderRepository;
        }

        public int? CreateOrder(Order order)
        {
            var entity = new entitieModel.Order()
            { 
                OrderCreatedDate = DateTime.Now,
                UserId = order.UserId
            };

            if (entity != null)
            {
                var existOrder = _orderRepository.Create(entity);

                foreach (var id in order.VehicleIds)
                {
                    var vehicleOrder = new entitieModel.VehicleOrder
                    {
                        OrderId = existOrder.Id,
                        VehicleId = id
                    };

                    _vehicleOrderRepository.Create(vehicleOrder);
                }

                return existOrder.Id;
            }

            return null;
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            var entitiList = _orderRepository.Get(o => o.UserId.Equals(userId)).ToList();
            var orders =  _mapper.Map<List<Order>>(entitiList);

            return orders;
        }

        public List<orderModel.OrderDetails> GetAll()
        {
            var entityList = _orderRepository.GetAll();

            var result = _mapper.Map<List<orderModel.OrderDetails>>(entityList);

            return result;
        }
    }
}