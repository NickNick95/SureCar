using AutoMapper;
using SureCar.Services.Interface;
using SureCar.Repositories.Interfaces;
using entitieModel = SureCar.Entities;
using SureCar.Services.Models;

namespace SureCar.Services
{
    public class WherehouseService : IWarehouseService
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseRepository _warehouseRepository;

        public WherehouseService(IMapper mapper,
            IWarehouseRepository warehouseRepository)
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
            var warehouses = _mapper.Map<List<Warehouse>>(data);

            return warehouses;
        }
    }
}
