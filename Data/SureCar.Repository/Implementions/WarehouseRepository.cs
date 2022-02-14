using SureCar.Entities;
using Microsoft.EntityFrameworkCore;
using SureCar.Repositories.Interfaces;

namespace SureCar.Repositories.Implementions
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        private readonly DataContext _context;

        public WarehouseRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Warehouse> GetAll()
        {
            var result = _context.Warehouses
                .Include(c => c.Car)
                .Include(v => v.Car.Vehicles)
                .Include(l => l.Location)
                .ToList();

            return result;
        }
    }
}
