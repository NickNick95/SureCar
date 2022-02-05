using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SureCar.Common.Interface.DataManager;
using SureCar.DataManagers;
using SureCar.Repositories;
using SureCar.Repositories.Implementions;
using SureCar.Repositories.Interfaces;
using SureCar.Services;
using SureCar.Services.Interface;

namespace SureCar.API.Infrastructure
{
    public class Registry
    {
        public static void BuildServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new AutoMapperProfile())).CreateMapper());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<DatabaseManager>();
            services.AddTransient<DbContext, DataContext>();
            services.AddTransient<IJsonDataManager, JsonDataManager>();


            services.AddTransient<IWarehouseService, WherehouseService>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
        }
    }
}
