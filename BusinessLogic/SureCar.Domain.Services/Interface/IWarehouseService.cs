using SureCar.Services.Models;

namespace SureCar.Services.Interface
{
    public interface IWarehouseService
    {
        void AddAll(List<Warehouse> warehouse);
        List<Warehouse> GetAll();
    }
}
