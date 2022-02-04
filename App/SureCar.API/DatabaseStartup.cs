using SureCar.Common.Interface.DataManager;
using SureCar.DataManager.Models;
using SureCar.Repositories;

namespace SureCar.API
{
    public class DatabaseStartup
    {
        private const string _fileName = "warehouses.json";

        public static void PrepareDatabaseIfNotExists(IServiceProvider provider)
        {
            var jsonDataManager = provider.GetService<IJsonDataManager>();
            var context = provider.GetService<DataContext>();

            if (!context.Database.Exists())
            {
                var data = GetDefaultData(jsonDataManager);
                context.Database.CreateIfNotExists();
            }
        }

        private static List<Warehouse> GetDefaultData(IJsonDataManager jsonDataManager)
        {
            var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, _fileName);
            var warehouses = jsonDataManager.ParseJsonFromFile<List<Warehouse>>(path);

            return warehouses;
        }
    }
}
