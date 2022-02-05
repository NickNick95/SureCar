using SureCar.Entities;

namespace SureCar.Repositories.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        List<Warehouse> GetAll();
    }
}
