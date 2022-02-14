using SureCar.Services.Models;
using orderModel = SureCar.Services.Models.OrderModel;

namespace SureCar.Services.Interface
{
    public interface IOrderService
    {
        public int? CreateOrder(Order order);

        List<orderModel.OrderDetails> GetAll();
    }
}
