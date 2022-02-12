using AutoMapper;
using SureCar.Repositories.Interfaces;
using SureCar.Services.Interface;
using SureCar.Services.Models;
using entitieModel = SureCar.Entities;

namespace SureCar.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<entitieModel.Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<entitieModel.Order> orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        public bool CreateOrder(Order order)
        {
            var entity = _mapper.Map<entitieModel.Order>(order);
            if (entity != null)
            {
                _orderRepository.Create(entity);
                return true;
            }

            return false;
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            var entitiList = _orderRepository.Get(o => o.UserId.Equals(userId)).ToList();
            var orders =  _mapper.Map<List<Order>>(entitiList);

            return orders;
        }
    }
}
