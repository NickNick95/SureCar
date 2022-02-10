using AutoMapper;
using SureCar.Services.Interface;
using SureCar.Repositories.Interfaces;
using entitieModel = SureCar.Entities;
using SureCar.Services.Models;

namespace SureCar.Services
{
    public class WherehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public WherehouseService(IWarehouseRepository warehouseRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
        }

        public void AddAll(List<Warehouse> warehouses)
        {
            var warehouseEntities = _mapper.Map<List<entitieModel.Warehouse>>(warehouses);

            foreach (var warehouse in warehouseEntities)
                _warehouseRepository.Create(warehouse);
        }

        public List<Warehouse> GetAll()
        {
            var data = _warehouseRepository.GetAll();
            List<Warehouse> warehouses = _mapper.Map<List<Warehouse>>(data);

            foreach (var warehouse in warehouses)
            {
                warehouse.Car.Vehicles.Sort((x, y) => x.DateAdded.CompareTo(y.DateAdded));
            }
            return warehouses;
        }
    }
}
