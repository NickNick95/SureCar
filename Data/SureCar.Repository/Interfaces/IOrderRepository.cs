using SureCar.Entities;

namespace SureCar.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetAll();
    }
}
