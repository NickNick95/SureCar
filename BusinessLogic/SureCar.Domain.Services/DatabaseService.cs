using AutoMapper;
using SureCar.Common.Interface.DataManager;
using SureCar.DataManagers.Models;
using SureCar.Repositories;
using SureCar.Services.Interface;
using serviceModels = SureCar.Services.Models;

namespace SureCar.Services
{
    public class DatabaseService : IDatabaseService
    {

        private const string _fileName = "warehouses.json";

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IJsonDataManager _jsonDataManager;
        private readonly IWarehouseService _warehouseService;

        public DatabaseService(IMapper mapper,
            DataContext context,
            IUserService userService,
            IJsonDataManager jsonDataManager,
            IWarehouseService warehouseService)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
            _jsonDataManager = jsonDataManager;
            _warehouseService = warehouseService;
        }

        public async Task PrepareDatabaseIfNotExists()
        {
            if (!_context.Warehouses.Any())
            {
                await _userService.InitializeBaseAdminAsync();
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
