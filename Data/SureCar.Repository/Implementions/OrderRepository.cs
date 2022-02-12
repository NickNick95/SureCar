using Microsoft.EntityFrameworkCore;
using SureCar.Entities;
using SureCar.Repositories.Interfaces;

namespace SureCar.Repositories.Implementions
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            var result = _context.Orders
                .Include(o => o.User)
                .Include(v => v.VehicleOrders)
                .ThenInclude(v => v.Vehicle)
                .ToList();

            return result;
        }
    }
}
