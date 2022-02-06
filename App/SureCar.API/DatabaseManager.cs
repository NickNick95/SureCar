using AutoMapper;
using SureCar.Repositories;
using SureCar.DataManagers.Models;
using SureCar.Common.Interface.DataManager;
using SureCar.Services.Interface;
using serviceModels = SureCar.Services.Models;

namespace SureCar.API
{
    public class DatabaseManager
    {
        private const string _fileName = "warehouses.json";

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IJsonDataManager _jsonDataManager;
        private readonly IWarehouseService _warehouseService;

        public DatabaseManager(IMapper mapper,
            DataContext context,
            IJsonDataManager jsonDataManager,
            IWarehouseService warehouseService)
        {
            _mapper = mapper;
            _context = context;
            _jsonDataManager = jsonDataManager;
            _warehouseService = warehouseService;
        }

        public void PrepareDatabaseIfNotExists()
        {
            if (_context.Database.EnsureCreated())
            {
                var data = GetDefaultData();
                var warehouses = _mapper.Map<List<serviceModels.Warehouse>>(data);
                 _warehouseService.AddAll(warehouses);
            }
        }

        private List<Warehouse> GetDefaultData()
        {
            var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, _fileName);
            var warehouses = _jsonDataManager.ParseJsonFromFile<List<Warehouse>>(path);

            return warehouses;
        }
    }
}
