using SureCar.Services.Models;

namespace SureCar.Services.Interface
{
    public interface IOrderService
    {
        public bool CreateOrder(Order order);
    }
}
